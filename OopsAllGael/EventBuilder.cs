using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoulsFormats;

namespace OopsAllGael
{

    partial class EventBuilder
    {
        public EMEVD.Event event_;

        int cursor = 0;
        int paramCount = 0;

        public bool AtEnd => cursor == event_.Instructions.Count;
        public EMEVD.Instruction CurrentInstruction => AtEnd ? null : event_.Instructions[cursor];

        public EMEVD.Event Build()
        {
            return event_;
        }

        public EventBuilder Event(long id, EMEVD.Event.RestBehaviorType restBehavior)
        {
            event_ = new EMEVD.Event(id, restBehavior);
            return this;
        }

        public EventBuilder Event(EMEVD.Event event_, int paramCount)
        {
            this.event_ = event_;
            this.paramCount = paramCount;
            return this;
        }

        public EventBuilder Param<T>(EventArg<T> param)
        {
            if (!param.isParam)
            {
                throw new ArgumentException("Invalid parameter");
            }
            if (param.index != null)
            {
                throw new ArgumentException("Parameter already initialized");
            }
            param.index = paramCount++;
            return this;
        }

        public EventBuilder Noop()
        {
            Remove();
            SkipUnconditionally(0);
            return this;
        }

        public EventBuilder Seek(int index)
        {
            if (index > event_.Instructions.Count || index < 0)
            {
                throw new IndexOutOfRangeException();
            }
            cursor = index;
            return this;
        }

        public EventBuilder Skip(int offset)
        {
            return Skip(offset, out _);
        }
        public EventBuilder Skip(int offset, out int skipped)
        {
            if (cursor + offset > event_.Instructions.Count)
            {
                skipped = event_.Instructions.Count - cursor;
                cursor = event_.Instructions.Count;
            }
            else if (cursor + offset < 0)
            {
                skipped = -cursor;
                cursor = 0;
            }
            else
            {
                skipped = offset;
                cursor += offset;
            }
            return this;
        }

        public EventBuilder Remove()
        {
            return Remove(out _);
        }
        public EventBuilder Remove(out EMEVD.Instruction instr)
        {
            if (AtEnd)
            {
                throw new IndexOutOfRangeException();
            }
            instr = event_.Instructions[cursor];
            event_.Parameters.RemoveAll(param => param.InstructionIndex == cursor);
            Util.ShiftParams(event_.Parameters, cursor, -1);
            event_.Instructions.RemoveAt(cursor);
            if (cursor > event_.Instructions.Count)
            {
                cursor = event_.Instructions.Count;
            }
            return this;
        }

        public void Insert(EMEVD.Instruction instr)
        {
            event_.Instructions.Insert(cursor, instr);
            cursor++;

        }

        /*
        public EventBuilder Find(Predicate<MyInstr> pred)
        {
            while (!AtEnd)
            {
                MyInstr myInstr = MyInstr.FromInstr(CurrentInstruction, cursor, event_.Parameters);
                bool ret = pred(myInstr);
                if (!ret)
                {
                    Skip(1);
                }
                else
                {
                    Remove();
                    Insert(myInstr.ToInstr(cursor, event_.Parameters, paramCount));
                    Skip(-1);
                    break;
                }
            }
            return this;
        }
        */

        public EventBuilder ForEach(Predicate<MyInstr> pred)
        {
            while (!AtEnd)
            {
                MyInstr myInstr = MyInstr.FromInstr(CurrentInstruction, cursor, event_.Parameters);
                bool ret = pred(myInstr);
                if (ret)
                {
                    Remove();
                    Insert(myInstr.ToInstr(cursor, event_.Parameters, paramCount));
                }
                else
                {
                    Skip(1);
                }
            }
            return this;
        }

        public EventBuilder Modify(Action<MyInstr> act)
        {
            MyInstr myInstr = MyInstr.FromInstr(CurrentInstruction, cursor, event_.Parameters);
            act(myInstr);
            Remove();
            Insert(myInstr.ToInstr(cursor, event_.Parameters, paramCount));
            return this;
        }

        object MakeArgObject<T>(EventArg<T> arg, EMEVD.Instruction.ArgType argType, IEnumerable<object> precedingArgs)
        {
            if (arg.isParam)
            {
                if (arg.index == null || arg.index < 0 || arg.index >= paramCount)
                {
                    throw new ArgumentException("Parameter does not exist");
                }

                EMEVD.Instruction dummyInstr = new EMEVD.Instruction(0, 0, precedingArgs);
                EMEVD.Parameter param = new EMEVD.Parameter(event_.Instructions.Count, dummyInstr.ArgData.Length, (long)arg.index * 4, 4);
                event_.Parameters.Add(param);
            }

            return arg.ToObject(argType);
        }

        

        /*

        public InitializeEventBuilder InitializeEvent(EventArg<int> eventSlotId, EventArg<uint> eventId, uint? layerMask = null)
        {
            List<object> args = new List<object>();
            args.Add(MakeArgObject(eventSlotId, EMEVD.Instruction.ArgType.Int32, args));
            args.Add(MakeArgObject(eventId, EMEVD.Instruction.ArgType.UInt32, args));
            return new InitializeEventBuilder(this, InstrType.InitializeEvent, layerMask, args);
        }

        public InitializeEventBuilder InitializeCommonEvent(EventArg<uint> eventId, uint? layerMask = null)
        {
            List<object> args = new List<object>();
            args.Add(MakeArgObject(eventId, EMEVD.Instruction.ArgType.UInt32, args));
            return new InitializeEventBuilder(this, InstrType.InitializeCommonEvent, layerMask, args);
        }

        public class InitializeEventBuilder
        {
            readonly EventBuilder parent;
            InstrType instrType;
            List<object> args = new List<object>();
            uint? layerMask;

            public InitializeEventBuilder(EventBuilder parent, InstrType instrType, uint? layerMask, IEnumerable<object> initialArgs)
            {
                this.parent = parent;
                this.instrType = instrType;
                args.AddRange(initialArgs);
                this.layerMask = layerMask;
            }

            public InitializeEventBuilder Arg<T>(EventArg<T> arg)
            {
                args.Add(parent.MakeArgObject(arg, Util.ArgTypeFromType<T>(), args));
                return this;
            }

            public EventBuilder Build()
            {
                EMEVD.Instruction instr = new EMEVD.Instruction((int)instrType >> 8, (int)instrType & 0xFF, layerMask, args);
                parent.Insert(instr);
                return parent;
            }
        }
        */
    }

    abstract partial class MyInstr
    {
        public uint? layerMask;
        public readonly InstrType instrType;
        protected EMEVD.Instruction.ArgType[] _argTypes;

        public MyInstr(InstrType instrType, uint? layerMask)
        {
            this.instrType = instrType;
            this.layerMask = layerMask;
        }

        public abstract AbstractEventArg[] gatherArgs();
        public EMEVD.Instruction ToInstr(int index, List<EMEVD.Parameter> parameters, int paramCount = -1)
        {
            Util.ShiftParams(parameters, index, 1);
            AbstractEventArg[] args = gatherArgs();
            int packedSize = 0;
            List<object> objects = new List<object>();
            for (int i = 0; i < args.Length; i++)
            {
                int size = Util.ArgTypeToSize(_argTypes[i]);
                int remainder = packedSize % size;
                packedSize += (size - remainder) % size;
                if (args[i].isParam)
                {
                    if (paramCount != -1 && args[i].index == null || args[i].index < 0 || args[i].index >= paramCount)
                    {
                        throw new ArgumentException("Invalid parameter");
                    }

                    EMEVD.Parameter param = new EMEVD.Parameter(index, packedSize, (long)args[i].index * 4, 4);
                    parameters.Add(param);
                }
                packedSize += size;
                objects.Add(args[i].ToObject(_argTypes[i]));
            }
            return new EMEVD.Instruction((int)instrType >> 8, (int)instrType & 0xFF, objects) { Layer = layerMask };
        }
    }

    public abstract class AbstractEventArg
    {
        public bool isParam;
        public int? index;
        abstract public object ToObject(EMEVD.Instruction.ArgType argType);
    }

    public class EventArg<T> : AbstractEventArg
    {
        readonly T value;
        public T Value => !isParam ? value : throw new ArgumentException();
        
        public override object ToObject(EMEVD.Instruction.ArgType argType) => Util.ObjectToArgType(index == null ? (object) value : 0, argType);
        public static implicit operator EventArg<T>(T arg) => new EventArg<T>(arg);
        public static explicit operator T(EventArg<T> arg) => arg.Value;

        EventArg(T value)
        {
            this.value = value;
            isParam = false;
        }

        public EventArg()
        {
            isParam = true;
        }
    }

}
