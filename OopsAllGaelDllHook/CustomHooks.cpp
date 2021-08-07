#include "pch.h"
#include "CustomHooks.h"
#include <limits>

struct DS3Enemy
{
	char _unk0[0x48];
	wchar_t ModelName[16];
	char _unk1[0xdc - 0x48 - sizeof(wchar_t) * 16];
	int MaxHP;
};

int MyGetMaxHP(struct DS3Enemy* EnemyPtr)
{
	DBUG_LOG("Enemy ptr: %p; %S; %d\n", EnemyPtr, EnemyPtr->ModelName, EnemyPtr->MaxHP);

	if (wcsncmp(EnemyPtr->ModelName, L"c6200", 5) == 0)
	{
		double ratio = 0.65;
		return (int)std::floor(EnemyPtr->MaxHP / ratio);
	}
	else if (wcsncmp(EnemyPtr->ModelName, L"c6201", 5) == 0)
	{
		double ratio = (0.35 * 2776) / 1031;
		return (int) std::floor(EnemyPtr->MaxHP / ratio);
	}

	return EnemyPtr->MaxHP;
}

void* CreateRelayFunction(void* InstrAddr, void* NewFuncAddr)
{
	SYSTEM_INFO si;
	GetSystemInfo(&si);
	
	MEMORY_BASIC_INFORMATION mbi;
	VirtualQuery(InstrAddr, &mbi, sizeof(MEMORY_BASIC_INFORMATION));

	BYTE* p = (BYTE*)mbi.AllocationBase;
	void* RelayMem = 0;
	int MaxRetries = 1000;
	for (int i = 0; i < MaxRetries && RelayMem == 0; i++)
	{
		// find block of free memory close to instruction
		do
		{
			p += si.dwAllocationGranularity;
			VirtualQuery(p, &mbi, sizeof(MEMORY_BASIC_INFORMATION));
		} while (mbi.State != MEM_FREE);

		// make sure memory is in range of X86 CALL instruction
		if (p > (BYTE*)InstrAddr + std::numeric_limits<int>::max())
			break;

		// race condition; should succeed, but retry if it doesn't
		RelayMem = VirtualAlloc(p, si.dwAllocationGranularity, MEM_RESERVE | MEM_COMMIT, PAGE_EXECUTE_READWRITE);
	}

	if (RelayMem == 0)
	{
		MessageBoxA(NULL, "Failed to allocate memory", "Hook Error", MB_OK);
		exit(1);
	}

	DBUG_LOG("Page size: %d\n", si.dwPageSize);
	DBUG_LOG("RelayMem: %p\n", RelayMem);

	// write JMP and address
	BYTE JmpInstr[] = { 0xFF, 0x25, 0x00, 0x00, 0x00, 0x00 }; // JMP [RIP]
	UINT64* RelayAddressPtr = (UINT64*) (((BYTE*)RelayMem) + 6);
	memcpy(RelayMem, JmpInstr, 6);
	*RelayAddressPtr = (UINT64)NewFuncAddr;

	return RelayMem;
}

void ReplaceCallAddress(void* InstrAddr, void* NewFuncAddr)
{
	int* OffsetAddr = (int*)((BYTE*)InstrAddr + 1);

	DBUG_LOG("Old call offset: %d\n", *OffsetAddr);
	DBUG_LOG("New func address: %p\n", NewFuncAddr);

	void* RelayAddr = CreateRelayFunction(InstrAddr, NewFuncAddr);
	int NewOffset = (int)((UINT64)RelayAddr - ((UINT64)InstrAddr + 5));

	DBUG_LOG("New call offset: %d\n", NewOffset);

	// overwrite CALL address
	DWORD OldProtection;
	VirtualProtect(InstrAddr, 5, PAGE_READWRITE, &OldProtection);
	*OffsetAddr = NewOffset;
	VirtualProtect(InstrAddr, 5, OldProtection, &OldProtection);
}

void SetupHooks()
{
	// Create a console for Debug output
#if _DEBUG
	AllocConsole();
#endif

	void* GetMaxHpInstrAddr = (void*)0x1409b2024;
	BYTE ExpectedBytes[] = { 0xe8, 0xd7, 0x0b, 0xa0, 0xff };
	if (memcmp(GetMaxHpInstrAddr, ExpectedBytes, 5) != 0)
	{
		MessageBoxA(NULL, "Unexpected instruction", "Hook Error", MB_OK);
		exit(1);
	}
	ReplaceCallAddress(GetMaxHpInstrAddr, MyGetMaxHP);
}