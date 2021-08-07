#pragma once

#include <cstdio>

#ifdef _DEBUG
#define DBUG_LOG(...) {	\
	char buf[1024];	\
	snprintf(buf, 1024, __VA_ARGS__);	\
	WriteConsoleA(GetStdHandle(STD_OUTPUT_HANDLE), buf, (DWORD)strlen(buf), nullptr, nullptr); \
}
#else
#define DBUG_LOG(...)
#endif