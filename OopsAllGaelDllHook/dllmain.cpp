// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include "DInput8.h"
#include "Hook.h"
#include "CustomHooks.h"

void Init()
{
    // Load the original dinput8.dll from the system directory
    char DInputDllName[MAX_PATH];
    GetSystemDirectoryA(DInputDllName, MAX_PATH);
    strcat_s(DInputDllName, "\\dinput8.dll");
    DInput8DLL = LoadLibraryA(DInputDllName);
    if (DInput8DLL > (HMODULE)31)
    {
        OriginalFunction = (DirectInput8Create_t)GetProcAddress(DInput8DLL, "DirectInput8Create");
    }
    InitializeHooking();

    SetupHooks();
}

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        Init();
        break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

