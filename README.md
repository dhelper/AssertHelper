AssertHelper
============
One assert to rule them all!

Use Expect.That and use expressions to tell your unit testing what you want to do.

Examples:
-------------------------------------------------
* Expect.That(value == true) --> Assert.IsTrue
* Expect.That(value == false) --> Assert.IsFalse
* Expect.That(value == null) --> Assert.IsNull
* Expect.That(value != null) --> Assert.IsNotNull
* Expect.That(result == expected) --> Assert.AreEqual
* Expect.That(result != expected) --> Assert.AreNotEqual
* Expect.That(result is expectedType) --> Assert.IsInstanceOfType
* Expect.That(aString.StartsWith(subString))	--> StringAssert.StartsWith
* Expect.That(aString.EndsWith(subString)) --> StringAssert.EndsWith
* Expect.That(aString.Contains(subString)) --> StringAssert.Contains
* Expect.That(aCollection == expected) --> CollectionAssert.AreEqual
* Expect.That(aCollection != expected) --> CollectionAssert.AreNotEqual
* Expect.That(aCollection.Contains(value)) --> CollectionAssert.Contains

Currently supports NUnit & MSTest
