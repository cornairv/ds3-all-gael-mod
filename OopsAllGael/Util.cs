using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;

namespace OopsAllGael
{
    class Util
    {
        // make a deep copy
        public static MSB3.Part.Enemy CopyEnemy(MSB3.Part.Enemy other)
        {
            MSB3.Part.Enemy enemy = new MSB3.Part.Enemy();
            other.BackreadGroups.CopyTo(enemy.BackreadGroups, 0);
            enemy.BackupEventAnimID = other.BackupEventAnimID;
            enemy.CharaInitID = other.CharaInitID;
            enemy.CollisionName = other.CollisionName;
            enemy.DisablePointLightEffect = other.DisablePointLightEffect;
            other.DispGroups.CopyTo(enemy.DispGroups, 0);
            enemy.DrawByReflectCam = other.DrawByReflectCam;
            other.DrawGroups.CopyTo(enemy.DrawGroups, 0);
            enemy.DrawOnlyReflectCam = other.DrawOnlyReflectCam;
            other.EntityGroups.CopyTo(enemy.EntityGroups, 0);
            enemy.EntityID = other.EntityID;
            enemy.Gparam = other.Gparam;
            enemy.IsShadowOnly = other.IsShadowOnly;
            enemy.LanternID = other.LanternID;
            enemy.LodParamID = other.LodParamID;
            enemy.MapStudioLayer = other.MapStudioLayer;
            enemy.ModelName = other.ModelName;
            enemy.Name = other.Name;
            enemy.NPCParamID = other.NPCParamID;
            enemy.PlatoonID = other.PlatoonID;
            enemy.PointLightShadowSource = other.PointLightShadowSource;
            enemy.PointMoveType = other.PointMoveType;
            enemy.Position = other.Position;
            enemy.Rotation = other.Rotation;
            enemy.Scale = other.Scale;
            enemy.ShadowDest = other.ShadowDest;
            enemy.ShadowSource = other.ShadowSource;
            enemy.SibPath = other.SibPath;
            enemy.TalkID = other.TalkID;
            enemy.ThinkParamID = other.ThinkParamID;
            enemy.UnkE04 = other.UnkE04;
            enemy.UnkE05 = other.UnkE05;
            enemy.UnkE0E = other.UnkE0E;
            enemy.UnkE18 = other.UnkE18;
            enemy.UnkT78 = other.UnkT78;
            enemy.UnkT84 = other.UnkT84;
            enemy.UseDepthBiasFloat = other.UseDepthBiasFloat;
            enemy.WalkRouteName = other.WalkRouteName;
            return enemy;
        }

        public static PARAM.Row CopyRow(PARAM.Row other, PARAMDEF paramdef)
        {
            PARAM.Row row = new PARAM.Row(other.ID, other.Name, paramdef);
            for (int i = 0; i < other.Cells.Count; i++)
            {
                row.Cells[i].Value = other.Cells[i].Value;
            }
            return row;
        }

        public static void SetCellValue(PARAM.Row row, string key, object value)
        {
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Def.DisplayName == key)
                {
                    row.Cells[i].Value = value;
                    return;
                }
            }
            throw new KeyNotFoundException(key);
        }

        public static T GetCellValue<T>(PARAM.Row row, string key)
        {
            foreach (PARAM.Cell cell in row.Cells)
            {
                if (cell.Def.DisplayName == key)
                {
                    return (T)(cell.Value);
                }
            }
            throw new KeyNotFoundException(key);
        }

        public static void ModifyCellValue<T>(PARAM.Row row, string key, Func<T,T> modifier)
        {
            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Def.DisplayName == key)
                {
                    row.Cells[i].Value = modifier((T)row.Cells[i].Value);
                    return;
                }
            }
            throw new KeyNotFoundException(key);
        }

        public static void InsertRow(PARAM param, PARAM.Row new_row)
        {
            int i = param.Rows.FindIndex(row => row.ID > new_row.ID);
            if (i == -1)
            {
                param.Rows.Add(new_row);
            }
            else
            {
                param.Rows.Insert(i, new_row);
            }
        }

        public static object ObjectToArgType(object o, EMEVD.Instruction.ArgType argType)
        {
            switch (argType)
            {
                case EMEVD.Instruction.ArgType.Byte:
                    return Convert.ToByte(o);
                case EMEVD.Instruction.ArgType.SByte:
                    return Convert.ToSByte(o);
                case EMEVD.Instruction.ArgType.Int16:
                    return Convert.ToInt16(o);
                case EMEVD.Instruction.ArgType.UInt16:
                    return Convert.ToUInt16(o);
                case EMEVD.Instruction.ArgType.Int32:
                    return Convert.ToInt32(o);
                case EMEVD.Instruction.ArgType.UInt32:
                    return Convert.ToUInt32(o);
                case EMEVD.Instruction.ArgType.Single:
                    return Convert.ToSingle(o);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static EMEVD.Instruction.ArgType ArgTypeFromType<T>()
        {
            Type t = typeof(T);
            if (t == typeof(byte))
                return EMEVD.Instruction.ArgType.Byte;
            else if (t == typeof(sbyte))
                return EMEVD.Instruction.ArgType.SByte;
            else if (t == typeof(short))
                return EMEVD.Instruction.ArgType.Int16;
            else if (t == typeof(ushort))
                return EMEVD.Instruction.ArgType.UInt16;
            else if (t == typeof(int) || t == typeof(Enum))
                return EMEVD.Instruction.ArgType.Int32;
            else if (t == typeof(uint))
                return EMEVD.Instruction.ArgType.UInt32;
            else if (t == typeof(float))
                return EMEVD.Instruction.ArgType.Single;
            else
                throw new ArgumentOutOfRangeException();
        }

        public static int ArgTypeToSize(EMEVD.Instruction.ArgType argType)
        {
            switch (argType)
            {
                case EMEVD.Instruction.ArgType.Byte:
                case EMEVD.Instruction.ArgType.SByte:
                    return 1;
                case EMEVD.Instruction.ArgType.Int16:
                case EMEVD.Instruction.ArgType.UInt16:
                    return 2;
                case EMEVD.Instruction.ArgType.Int32:
                case EMEVD.Instruction.ArgType.UInt32:
                case EMEVD.Instruction.ArgType.Single:
                    return 4;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static int GetPackedArgsLength(IEnumerable<EMEVD.Instruction.ArgType> args)
        {
            int packedSize = 0;
            foreach (EMEVD.Instruction.ArgType arg in args)
            {
                int size = ArgTypeToSize(arg);
                int remainder = packedSize % size;
                packedSize += (size - remainder) % size;
            }
            return packedSize;
        }

        // index in instruction args => index in event parameters
        public static int[] UnpackParams(List<EMEVD.Parameter> parameters, int index, EMEVD.Instruction.ArgType[] argTypes)
        {
            if (argTypes.Length == 0)
                return new int[0];

            parameters = parameters.FindAll(param => param.InstructionIndex == index);
            parameters.Sort((p1, p2) => (int) p1.TargetStartByte - (int) p2.TargetStartByte);

            int[] paramMap = Enumerable.Repeat(-1, argTypes.Length).ToArray();
            for (int i = 0, j = 0, packedSize = 0; i < argTypes.Length && j < parameters.Count; i++)
            {
                int size = ArgTypeToSize(argTypes[i]);
                int remainder = packedSize % size;
                packedSize += (size - remainder) % size;
                if (parameters[j].TargetStartByte == packedSize)
                {
                    paramMap[i] = (int)parameters[j].SourceStartByte / 4;
                    j++;
                }
                packedSize += size;
            }
            return paramMap;
        }

        public static void ShiftParams(List<EMEVD.Parameter> parameters, int index, int offset)
        {
            foreach (EMEVD.Parameter param in parameters)
            {
                if (param.InstructionIndex >= index)
                {
                    param.InstructionIndex += offset;
                }
            }
        }

        public static int ReplaceInByteArray(byte[] input, byte[] pattern, byte[] replacement)
        {
            if (pattern.Length != replacement.Length)
            {
                throw new ArgumentException();
            }

            int matchLength = 0;
            int matchCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == pattern[matchLength])
                {
                    matchLength++;
                    if (matchLength == pattern.Length)
                    {
                        replacement.CopyTo(input, i - matchLength + 1);
                        matchLength = 0;
                        matchCount++;
                    }
                }
                else
                {
                    i -= matchLength;
                    matchLength = 0;
                }
            }
            return matchCount;
        }

        public static int ReplaceCXXXXInByteArray(byte[] input, byte[] replacement, char prefix = 'c')
        {
            bool unicode;
            if (replacement.Length == 5)
            {
                unicode = false;
            }
            else if (replacement.Length == 10)
            {
                unicode = true;
            }
            else
            {
                throw new ArgumentException();
            }

            int matchLength = 0;
            int matchCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (unicode && matchLength % 2 == 1)
                {
                    if (input[i] == 0)
                    {
                        matchLength++;
                    }
                    else
                    {
                        i -= matchLength;
                        matchLength = 0;
                    }
                }
                else if (matchLength == 0 && input[i] == prefix)
                {
                    matchLength++;
                }
                else if (matchLength > 0 && input[i] >= '0' && input[i] <= '9')
                {
                    matchLength++;
                }
                else
                {
                    i -= matchLength;
                    matchLength = 0;
                }

                if (matchLength == replacement.Length)
                {
                    replacement.CopyTo(input, i - matchLength + 1);
                    matchLength = 0;
                    matchCount++;
                }
            }
            return matchCount;
        }

        public static int ReplaceCXXXX_XXXXInByteArray(byte[] input, byte[] replacement, char prefix = 'c')
        {
            bool unicode;
            if (replacement.Length == 10)
            {
                unicode = false;
            }
            else if (replacement.Length == 20)
            {
                unicode = true;
            }
            else
            {
                throw new ArgumentException();
            }

            int matchLength = 0;
            int matchCount = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (unicode && matchLength % 2 == 1)
                {
                    if (input[i] == 0)
                    {
                        matchLength++;
                    }
                    else
                    {
                        i -= matchLength;
                        matchLength = 0;
                    }
                }
                else if (matchLength == 0 && input[i] == prefix)
                {
                    matchLength++;
                }
                else if (matchLength > 0 && input[i] >= '0' && input[i] <= '9')
                {
                    matchLength++;
                }
                else
                {
                    i -= matchLength;
                    matchLength = 0;
                }

                if (matchLength == replacement.Length / 2)
                {
                    replacement.CopyTo(input, i - matchLength + 1);
                    i += replacement.Length - matchLength;
                    matchLength = 0;
                    matchCount++;
                }
            }
            return matchCount;
        }


        public static InstrType GetInstrType(EMEVD.Instruction instr)
        {
            return (InstrType)((instr.Bank << 8) | instr.ID);
        }
    }
}
