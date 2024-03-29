﻿using AutoFixture;
using AutoFixture.Kernel;
using System.Reflection;

namespace TestingWithDb.IntegrationTests.Setup;

public class NoCircularReferencesCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(behavior => fixture.Behaviors.Remove(behavior));
    }
}

public class IgnoreVirtualMembersCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customizations.Add(new IgnoreVirtualMembers());
    }
}

public class IgnoreVirtualMembers : ISpecimenBuilder
{
    public object? Create(object request, ISpecimenContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException("context");
        }

        var propertyInfo = request as PropertyInfo;
        if (propertyInfo == null)
        {
            return new NoSpecimen();
        }

        if (propertyInfo.GetMethod != null && propertyInfo.GetMethod.IsVirtual)
        {
            return null;
        }

        return new NoSpecimen();
    }
}
