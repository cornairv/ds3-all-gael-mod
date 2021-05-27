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

        // make a deepish copy
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

    }
}
