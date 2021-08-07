#pragma once

#define WIN32_LEAN_AND_MEAN             // Exclude rarely-used stuff from Windows headers
#define NOMINMAX

// Windows Header Files
#include <windows.h>
#include <winternl.h>
#include <imagehlp.h>
#include <cmath>
#include "MyDebug.h"

#ifdef DLL_EXPORT
#define DINPUT8_API __declspec(dllexport)
#else
#define DINPUT8_API __declspec(dllimport)
#endif