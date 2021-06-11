using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SoulsFormats;

namespace OopsAllGael
{
    using AnimThreadArgs = Tuple<int, byte[], byte[], byte[], byte[], DCX.Type>;
    using ModelThreadArgs = Tuple<int, byte[], byte[], byte[], DCX.Type>;


    partial class Gaelifier
    {
        static HashSet<int> gaelAnimIds = new HashSet<int>(); // valid animation IDs for Gael phase 1

        public static void GetGaelAnimIds()
        {
            BND4 bnd = BND4.Read(@"chr\c6201.anibnd.dcx");
            BinderFile bndf = bnd.Files.Find(f => f.Name.EndsWith(".tae", StringComparison.OrdinalIgnoreCase)) ?? throw new KeyNotFoundException();
            TAE3 tae = TAE3.Read(bndf.Bytes);
            foreach (TAE3.Animation anim in tae.Animations)
            {
                Console.WriteLine(anim.ID);
                gaelAnimIds.Add((int)anim.ID % 1000000);
            }
        }

        public static void InsertModels()
        {
            animIds.Remove(0);
            animIds.Remove(6200);
            animIds.Remove(6201);
            modelIds.Remove(0);
            modelIds.Remove(6200);
            modelIds.Remove(6201);

            byte[] patternUnicode = Encoding.Unicode.GetBytes("c6201");
            byte[] patternUTF8 = Encoding.UTF8.GetBytes("c6201");
            byte[] gaelAniDcx = DCX.Decompress(@"chr\c6201.anibnd.dcx", out DCX.Type compressionType);
            byte[] gaelBehDcx = DCX.Decompress(@"chr\c6201.behbnd.dcx");
            List<Thread> threads = new List<Thread>();
            foreach (int animId in animIds)
            {
                Thread t = new Thread(AnimThread);
                t.Start(new AnimThreadArgs(animId, patternUnicode, patternUTF8, gaelAniDcx, gaelBehDcx, compressionType));
                threads.Add(t);
            }

            byte[] gaelChrDcx = DCX.Decompress(@"chr\c6201.chrbnd.dcx");
            foreach (int modelId in modelIds)
            {
                Thread t = new Thread(ModelThread);
                t.Start(new ModelThreadArgs(modelId, patternUnicode, patternUTF8, gaelChrDcx, compressionType));
                threads.Add(t);
            }

            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        static void AnimThread(object arg)
        {
            AnimThreadArgs args = arg as AnimThreadArgs;
            int animId = args.Item1;
            byte[] patternUnicode = args.Item2;
            byte[] patternUTF8 = args.Item3;
            byte[] gaelAniDcx = args.Item4;
            byte[] gaelBehDcx = args.Item5;
            DCX.Type compressionType = args.Item6;

            string animIdStr = "c" + animId.ToString().PadLeft(4, '0');
            File.Copy(@"action\script\c6201.hks", Constants.MOD_DIR + @"action\script\" + animIdStr + ".hks", true);
            // replace id in files
            byte[] replacementUnicode = Encoding.Unicode.GetBytes(animIdStr);
            byte[] replacementUTF8 = Encoding.UTF8.GetBytes(animIdStr);
            byte[] gaelAniDcxHexed = (byte[])gaelAniDcx.Clone();
            Util.ReplaceInByteArray(gaelAniDcxHexed, patternUnicode, replacementUnicode);
            Util.ReplaceInByteArray(gaelAniDcxHexed, patternUTF8, replacementUTF8);

            // copy events from enemy animations to Gael's animations
            BND4 enemyBnd = BND4.Read(@"chr\" + animIdStr + ".anibnd.dcx");
            BND4 gaelBnd = BND4.Read(gaelAniDcxHexed);
            ModifyAnimation(gaelBnd, enemyBnd, animIdStr);
            gaelBnd.Write(Constants.MOD_DIR + @"chr\" + animIdStr + ".anibnd.dcx", compressionType);

            // copy behavior file
            byte[] gaelBehDcxHexed = (byte[])gaelBehDcx.Clone();
            Util.ReplaceInByteArray(gaelBehDcxHexed, patternUnicode, replacementUnicode);
            Util.ReplaceInByteArray(gaelBehDcxHexed, patternUTF8, replacementUTF8);
            DCX.Compress(gaelBehDcxHexed, compressionType, Constants.MOD_DIR + @"chr\" + animIdStr + ".behbnd.dcx");
        }

        static void ModelThread(object arg)
        {
            ModelThreadArgs args = arg as ModelThreadArgs;
            int modelId = args.Item1;
            byte[] patternUnicode = args.Item2;
            byte[] patternUTF8 = args.Item3;
            byte[] gaelChrDcx = args.Item4;
            DCX.Type compressionType = args.Item5;

            string modelIdStr = "c" + modelId.ToString().PadLeft(4, '0');
            byte[] replacementUnicode = Encoding.Unicode.GetBytes(modelIdStr);
            byte[] replacementUTF8 = Encoding.UTF8.GetBytes(modelIdStr);

            // copy model file
            byte[] gaelChrDcxHexed = (byte[])gaelChrDcx.Clone();
            Util.ReplaceInByteArray(gaelChrDcxHexed, patternUnicode, replacementUnicode);
            Util.ReplaceInByteArray(gaelChrDcxHexed, patternUTF8, replacementUTF8);
            DCX.Compress(gaelChrDcxHexed, compressionType, Constants.MOD_DIR + @"chr\" + modelIdStr + ".chrbnd.dcx");
        }

        // copy animations/events from bndToCopy to bnd
        static void ModifyAnimation(BND4 bnd, BND4 bndToCopy, string animIdStr)
        {
            List<BinderFile> animBndfsToCopy = new List<BinderFile>();
            Dictionary<string, float> animDurations = new Dictionary<string, float>(); // hkx filename => anim duration
            Dictionary<string, float> animToCopyDurations = new Dictionary<string, float>(); // hkx filename => anim duration
            BinderFile defaultAnim = bnd.Files.Find(f => f.Name.EndsWith("a000_007400.hkx")) ?? throw new MyDebug.AssertException();
            for (int i = 0; i < bndToCopy.Files.Count; i++)
            {
                BinderFile bndfToCopy = bndToCopy.Files[i];
                if (bndfToCopy.Name.EndsWith("skeleton.hkx", StringComparison.OrdinalIgnoreCase))
                {
                    // pass
                }
                else if (bndfToCopy.Name.EndsWith(".hkx", StringComparison.OrdinalIgnoreCase))
                {
                    string animFileName = Path.GetFileNameWithoutExtension(bndfToCopy.Name);
                    int n1 = int.Parse(animFileName.Substring(1, 3));
                    int n2 = int.Parse(animFileName.Substring(5));
                    if (n2 >= 100000)
                    {
                        // skip test animations
                        continue;
                    }
                    n2 += Constants.ANIM_OFFSET;
                    bndfToCopy.Name = bndfToCopy.Name.Substring(0, bndfToCopy.Name.Length - 15) +  string.Format("a{0:D3}_{1:D6}.hkx", n1, n2);
                    bndfToCopy.ID += Constants.ANIM_OFFSET;
                    animBndfsToCopy.Add(bndfToCopy);
                }
                else if (!bndfToCopy.Name.EndsWith(".tae", StringComparison.OrdinalIgnoreCase))
                {
                    MyDebug.Fail();
                }
            }
            bnd.Files.AddRange(animBndfsToCopy);

            BinderFile taeBndf = bnd.Files.Find(f => f.Name.EndsWith(".tae"));
            TAE3 tae = TAE3.Read(taeBndf.Bytes);
            TAE3 taeToCopy = TAE3.Read(bndToCopy.Files.Find(f => f.Name.EndsWith(".tae")).Bytes);
            List<TAE3.Animation> animsToCopy = new List<TAE3.Animation>();
            foreach (TAE3.Animation animToCopy in taeToCopy.Animations)
            {
                if (animToCopy.AnimFileReference)
                {
                    animToCopy.AnimFileUnk18 += Constants.ANIM_OFFSET;
                }
                if (animToCopy.AnimFileName != "")
                {
                    string animFileName = Path.GetFileNameWithoutExtension(animToCopy.AnimFileName);
                    int hktN1 = int.Parse(animFileName.Substring(1, 3));
                    int hktN2 = int.Parse(animFileName.Substring(5));
                    hktN2 += Constants.ANIM_OFFSET;
                    animToCopy.AnimFileName = string.Format("a{0:D3}_{1:D6}.hkt", hktN1, hktN2);
                }

                int n1 = (int)(animToCopy.ID / 1000000);
                int n2 = (int)(animToCopy.ID % 1000000);
                animToCopy.ID += Constants.ANIM_OFFSET;

                animsToCopy.Add(animToCopy);
            }
            tae.Animations.AddRange(animsToCopy);
            taeBndf.Bytes = tae.Write();
        }

        static float GetAnimDuration(byte[] bytes)
        {
            // check header
            int version = BitConverter.ToInt32(bytes, 0xc);
            MyDebug.Assert(version == 11);
            int sections = BitConverter.ToInt32(bytes, 0x14);
            MyDebug.Assert(sections == 3);
            string versionName = Encoding.ASCII.GetString(bytes, 0x28, 14);
            MyDebug.Assert(versionName == "hk_2014.1.0-r1");

            // check class section
            string classSectionName = Encoding.ASCII.GetString(bytes, 0x50, 14);
            MyDebug.Assert(classSectionName == "__classnames__");
            int classNameDataOffset = BitConverter.ToInt32(bytes, 0x64);
            int currentOffset = classNameDataOffset;
            Dictionary<int, string> classNames = new Dictionary<int, string>(); // offset => class name
            while (BitConverter.ToUInt16(bytes, currentOffset) != 0xffff)
            {
                currentOffset += 5;
                int stringLength = 0;
                for (; bytes[currentOffset + stringLength] != 0; stringLength++) { }
                string className = Encoding.ASCII.GetString(bytes, currentOffset, stringLength);
                classNames[currentOffset - classNameDataOffset] = className;
                currentOffset += stringLength + 1;
            }

            string dataSectionName = Encoding.ASCII.GetString(bytes, 0xd0, 8);
            MyDebug.Assert(dataSectionName == "__data__");
            int dataOffset = BitConverter.ToInt32(bytes, 0xe4);
            int virtualFixupsOffset = BitConverter.ToInt32(bytes, 0xf0);
            int exportsOffset = BitConverter.ToInt32(bytes, 0xf4);
            for (currentOffset = dataOffset + virtualFixupsOffset; currentOffset < dataOffset + exportsOffset; currentOffset += 12)
            {
                int offset = BitConverter.ToInt32(bytes, currentOffset);
                if (offset != -1)
                {
                    int nameOffset = BitConverter.ToInt32(bytes, currentOffset + 8);
                    string className = classNames[nameOffset];
                    if (className == "hkaSplineCompressedAnimation" || className == "hkaInterleavedUncompressedAnimation")
                    {
                        float duration = BitConverter.ToSingle(bytes, offset + dataOffset + 0x14);
                        return duration;
                    }
                }
            }

            throw new MyDebug.AssertException();
        }
    }
}
