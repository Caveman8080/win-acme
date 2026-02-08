using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Fclp
{
    /// <summary>
    /// Defines a fluent interface for settings up a command.
    /// </summary>
    public interface ICommandLineCommandFluent<TBuildType>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        ICommandLineCommandFluent<TBuildType> OnSuccess(Action<TBuildType> callback);

        /// <summary>
        /// Sets up an Option for a write-able property on the type being built.
        /// </summary>
        ICommandLineOptionBuilderFluent<TProperty> Setup<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties)] TProperty>(Expression<Func<TBuildType, TProperty>> propertyPicker);
    }
}