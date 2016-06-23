using System;

using Cosmos.Assembler;
using XSharp.Compiler;
using static XSharp.Compiler.XSRegisters;
using CPUx86 = Cosmos.Assembler.x86;

namespace Cosmos.IL2CPU.Plugs.System.Runtime.CompilerServices {
	[Plug(Target = typeof(global::System.Runtime.CompilerServices.RuntimeHelpers))]
	public static class RuntimeHelpersImpl {

		public static void cctor() {
			//TODO: do something
		}

		[Inline(TargetPlatform = TargetPlatform.x86)]
		public static void InitializeArray(Array array, RuntimeFieldHandle fldHandle) {
			// Arguments:
			//    Array aArray, RuntimeFieldHandle aFieldHandle
            XS.Set(XSRegisters.EDI, XSRegisters.EBP, sourceDisplacement: 0xC); // array
		    XS.Set(EDI, EDI, sourceIsIndirect: true);
            XS.Set(XSRegisters.ESI, XSRegisters.EBP, sourceDisplacement: 8);// aFieldHandle
            XS.Add(XSRegisters.EDI, 8);
		    XS.Push(EDI, isIndirect: true);
            XS.Add(XSRegisters.EDI, 4);
            XS.Set(EAX, EDI, sourceIsIndirect: true);
		    XS.Multiply(ESP, isIndirect: true, size: RegisterSize.Int32);
            XS.Pop(XSRegisters.ECX);
            XS.Set(XSRegisters.ECX, XSRegisters.EAX);
            XS.Set(XSRegisters.EAX, 0);
            XS.Add(XSRegisters.EDI, 4);

			XS.Label(".StartLoop");
			XS.Set(DL, ESI, sourceIsIndirect: true);
            XS.Set(EDI, DL, destinationIsIndirect: true);
			XS.Add(XSRegisters.EAX, 1);
            XS.Add(XSRegisters.ESI, 1);
            XS.Add(XSRegisters.EDI, 1);
			XS.Compare(XSRegisters.EAX, XSRegisters.ECX);
            XS.Jump(CPUx86.ConditionalTestEnum.Equal, ".EndLoop");
            XS.Jump(".StartLoop");

			XS.Label(".EndLoop");
		}
	}
}