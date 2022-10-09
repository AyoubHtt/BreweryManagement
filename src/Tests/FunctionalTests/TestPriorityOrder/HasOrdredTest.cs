using Xunit;

namespace FunctionalTests.TestPriorityOrder;

[TestCaseOrderer(OrdredTestConst.OrdererTypeName, OrdredTestConst.OrdererAssemblyName)]
public abstract class HasOrdredTest { }
