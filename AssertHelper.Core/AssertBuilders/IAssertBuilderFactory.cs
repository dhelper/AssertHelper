using System;
using System.Linq.Expressions;
using AssertHelper.Core.AssertBuilders.AssertMethods.Base;

namespace AssertHelper.Core.AssertBuilders
{
    /// <summary>
    /// Factory interface for creating framework-specific assertion method wrappers.
    /// </summary>
    /// <remarks>
    /// This interface defines factory methods for creating all types of assertion method wrappers
    /// used by AssertHelper. Each method returns a wrapper configured for a specific test framework
    /// (NUnit, MSTest, etc.) that knows how to generate the appropriate assertion call expression.
    /// </remarks>
    internal interface IAssertBuilderFactory
    {
        /// <summary>
        /// Creates an IsTrue assertion method wrapper.
        /// </summary>
        IUnaryAssertMethod CreateIsTrue();
        
        /// <summary>
        /// Creates an IsFalse assertion method wrapper.
        /// </summary>
        IUnaryAssertMethod CreateIsFalse();
        
        /// <summary>
        /// Creates an IsNull assertion method wrapper.
        /// </summary>
        IUnaryAssertMethod CreateIsNull();
        
        /// <summary>
        /// Creates an IsNotNull assertion method wrapper.
        /// </summary>
        IUnaryAssertMethod CreateIsNotNull();
        
        /// <summary>
        /// Creates an AreEqual assertion method wrapper.
        /// </summary>
        IBinaryAssertMethod CreateAreEqual();
        
        /// <summary>
        /// Creates an AreNotEqual assertion method wrapper.
        /// </summary>
        IBinaryAssertMethod CreateAreNotEqual();
        
        /// <summary>
        /// Creates an IsInstanceOfType assertion method wrapper.
        /// </summary>
        IBinaryAssertMethod<Type, Expression> CreateIsInstanceOfType();
        
        /// <summary>
        /// Creates a Fail assertion method wrapper for custom error messages.
        /// </summary>
        IUnaryAssertMethod<string> CreateFail();
        
        /// <summary>
        /// Creates a StringContains assertion method wrapper.
        /// </summary>
        IBinaryAssertMethod CreateStringContains();
        
        /// <summary>
        /// Creates a StringStartsWith assertion method wrapper.
        /// </summary>
        IBinaryAssertMethod CreateStringStartsWith();
        
        /// <summary>
        /// Creates a StringEndsWith assertion method wrapper.
        /// </summary>
        IBinaryAssertMethod CreateStringEndsWith();
        
        /// <summary>
        /// Creates a CollectionContains assertion method wrapper.
        /// </summary>
        IBinaryAssertMethod CreateCollectionContains();
        
        /// <summary>
        /// Creates a CollectionAreEqual assertion method wrapper.
        /// </summary>
        IBinaryAssertMethod CreateCollectionAreEqual();
        
        /// <summary>
        /// Creates a CollectionAreNotEqual assertion method wrapper.
        /// </summary>
        IBinaryAssertMethod CreateCollectionAreNotEqual();
    }
}