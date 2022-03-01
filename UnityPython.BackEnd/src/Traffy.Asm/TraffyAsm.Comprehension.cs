using System;
using Traffy.Objects;

namespace Traffy.Asm
{

    [Serializable]
    public class Comprehension
    {
        public bool hasCont;
        public TraffyLHS target;
        public TraffyAsm itr;
        [System.Diagnostics.CodeAnalysis.AllowNull] public TraffyAsm[] ifs;
        [System.Diagnostics.CodeAnalysis.AllowNull] public Comprehension next;

        public void exec(Frame frame, Action step)
        {
            if (ifs != null)
            {
                if (next != null)
                {
                    var rt_itr = itr.exec(frame).__iter__();
                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        target.exec(frame, rt_each);

                        for (int i = 0; i < ifs.Length; i++)
                        {
                            var rt_if = ifs[i].exec(frame);
                            if (!rt_if.__bool__())
                                goto next0;
                        }
                        next.exec(frame, step);
                    next0:;
                    }
                }
                else
                {
                    var rt_itr = itr.exec(frame).__iter__();
                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        target.exec(frame, rt_each);

                        for (int i = 0; i < ifs.Length; i++)
                        {
                            var rt_if = ifs[i].exec(frame);
                            if (!rt_if.__bool__())
                                goto next1;
                        }
                        step();
                    next1:;
                    }
                }
            }

            else
            {
                if (next != null)
                {
                    var rt_itr = itr.exec(frame).__iter__();
                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        target.exec(frame, rt_each);
                        next.exec(frame, step);
                    }
                }
                else
                {
                    var rt_itr = itr.exec(frame).__iter__();
                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        target.exec(frame, rt_each);
                        step();
                    }
                }
            }
        }

        public async MonoAsync<TrObject> cont(Frame frame, Action step)
        {
            if (ifs != null)
            {
                if (next != null)
                {
                    var rt_itr = (itr.hasCont
                        ? await itr.cont(frame) : itr.exec(frame))
                        .__iter__();

                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        if (target.hasCont)
                            await target.cont(frame, rt_each);
                        else
                            target.exec(frame, rt_each);

                        for (int i = 0; i < ifs.Length; i++)
                        {
                            var rt_if = ifs[i].hasCont ? await ifs[i].cont(frame) : ifs[i].exec(frame);
                            if (!rt_if.__bool__())
                                goto next0;
                        }
                        if (next.hasCont)
                            await next.cont(frame, step);
                        else
                            next.exec(frame, step);
                        next0:;
                    }
                }
                else
                {
                    var rt_itr = (itr.hasCont
                        ? await itr.cont(frame) : itr.exec(frame))
                        .__iter__();

                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        if (target.hasCont)
                            await target.cont(frame, rt_each);
                        else
                            target.exec(frame, rt_each);

                        for (int i = 0; i < ifs.Length; i++)
                        {
                            var rt_if = ifs[i].hasCont ? await ifs[i].cont(frame) : ifs[i].exec(frame);
                            if (!rt_if.__bool__())
                                goto next1;
                        }
                        step();
                    next1:;
                    }
                }
            }
            else
            {
                if (next != null)
                {
                    var rt_itr = (itr.hasCont
                        ? await itr.cont(frame) : itr.exec(frame))
                        .__iter__();

                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        if (target.hasCont)
                            await target.cont(frame, rt_each);
                        else
                            target.exec(frame, rt_each);
                        if (next.hasCont)
                            await next.cont(frame, step);
                        else
                            next.exec(frame, step);
                    }
                }
                else
                {
                    var rt_itr = (itr.hasCont
                        ? await itr.cont(frame) : itr.exec(frame))
                        .__iter__();

                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        if (target.hasCont)
                            await target.cont(frame, rt_each);
                        else
                            target.exec(frame, rt_each);
                        step();
                    }
                }
            }
            return RTS.object_none;
        }

        public async MonoAsync<TrObject> contYield(Frame frame, Func<MonoAsync<TrObject>> step)
        {
            if (ifs != null)
            {
                if (next != null)
                {
                    var rt_itr = (itr.hasCont
                        ? await itr.cont(frame) : itr.exec(frame))
                        .__iter__();

                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        if (target.hasCont)
                            await target.cont(frame, rt_each);
                        else
                            target.exec(frame, rt_each);

                        for (int i = 0; i < ifs.Length; i++)
                        {
                            var rt_if = ifs[i].hasCont ? await ifs[i].cont(frame) : ifs[i].exec(frame);
                            if (!rt_if.__bool__())
                                goto next0;
                        }
                        await next.contYield(frame, step);
                        next0:;
                    }
                }
                else
                {
                    var rt_itr = (itr.hasCont
                        ? await itr.cont(frame) : itr.exec(frame))
                        .__iter__();

                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        if (target.hasCont)
                            await target.cont(frame, rt_each);
                        else
                            target.exec(frame, rt_each);

                        for (int i = 0; i < ifs.Length; i++)
                        {
                            var rt_if = ifs[i].hasCont ? await ifs[i].cont(frame) : ifs[i].exec(frame);
                            if (!rt_if.__bool__())
                                goto next1;
                        }
                        await step();
                    next1:;
                    }
                }
            }
            else
            {
                if (next != null)
                {
                    var rt_itr = (itr.hasCont
                        ? await itr.cont(frame) : itr.exec(frame))
                        .__iter__();

                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        if (target.hasCont)
                            await target.cont(frame, rt_each);
                        else
                            target.exec(frame, rt_each);
                        await next.contYield(frame, step);
                    }
                }
                else
                {
                    var rt_itr = (itr.hasCont
                        ? await itr.cont(frame) : itr.exec(frame))
                        .__iter__();

                    while (rt_itr.MoveNext())
                    {
                        var rt_each = rt_itr.Current;
                        if (target.hasCont)
                            await target.cont(frame, rt_each);
                        else
                            target.exec(frame, rt_each);
                        await step();
                    }
                }
            }
            return RTS.object_none;
        }
    }
}