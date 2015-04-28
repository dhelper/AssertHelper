AssertHelper
============
One assert to rule them all!

Supports NUnit & MSTest

#### Use Expect.That and expressions to tell your unit testing what to check:
* Expect.That(() => value == true) → Assert.IsTrue
* Expect.That(() => value == false) → Assert.IsFalse
* Expect.That(() => value == null) → Assert.IsNull
* Expect.That(() => value != null) → Assert.IsNotNull
* Expect.That(() => result == expected) → Assert.AreEqual
* Expect.That(() => result != expected) → Assert.AreNotEqual
* Expect.That(() => result is expectedType) → Assert.IsInstanceOfType
* Expect.That(() => aString.StartsWith(subString)) → StringAssert.StartsWith
* Expect.That(() => aString.EndsWith(subString)) → StringAssert.EndsWith
* Expect.That(() => aString.Contains(subString)) → StringAssert.Contains
* Expect.That(() => aCollection == expected) → CollectionAssert.AreEqual
* Expect.That(() => aCollection != expected) → CollectionAssert.AreNotEqual
* Expect.That(() => aCollection.Contains(value)) → CollectionAssert.Contains

#### Run several assert using one call - and see them run independently:
Expect.That(a == 100 && b != null) → Assert.AreEqual(100, a) | Assert.IsNotNull(b)
