using System.Reflection;

namespace InventoryManagementSystem.App.Helpers;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}