# Reflection in C# - Attribute Reading

## Overview: Reflection
Reflection is an important mechanism for obtaining type information during program execution. It also allows obtaining information about an already running application, manipulating its types. It also allows for dynamic addition of types, values and objects to the application.
Classes that have access to the metadata of the running program are defined in the System.Reflection namespace.

Reflection capabilities:
- viewing attributes during program execution;
- checking data types
- creating type instances;
- performing late binding to methods and properties (the target method is searched for during program execution.);
- creating new types during the program and creating tasks with these types.

## Attributes

An attribute is a tag. It passes metadata to the runtime:
- compiler instructions,
- comments,
- method and class descriptions.

Attributes describe the behavior of elements:

- classes,
- methods,
- structures,
- enumerations,
- particular program components.

Types of attributes:

- predefined attributes;
  - AttributeUsage;
  - Conditional;
  - Obsolete;

- custom attributes.


## Reflection and dynamic handling of types and classes
On the topic of reflection, I have an example of a similar application here. 
The <a href="https://github.com/janluksoft/NET_ReflectionTypes">NET_ReflectionTypes</a> application
uses a reflection mechanism to read classes and dynamically operate on them. 
This is a de facto powerful mechanism for adding functionality to a program already at runtime.


## Program operation

This program written in .NET8 C# demonstrates reading attributes of multiple elements.
Two simple classes MyClassMarkedWithAttributes and Oblong are created. 
Their operation is irrelevant, but different attributes are attached to their 
different elements. There are standard attributes and those created by the user. 
There is an example of three extended user attribute definitions, all of which 
inherit from the abstract Attribute class.

The program code then reads their content, for types and individual methods.
The presented reflection techniques have many applications.

## Output

The following messages are displayed as output:

```

Demonstration of reflection and user attributes in .NET8

Attributes in Types:
  UserAttributes.SecondMyAttribute
    Info: SecondMyAttribute: "Class is empty for now"
  UserAttributes.FirstMyAttribute
    Info: FirstMyAttribute: "It is my class", "Topic: swimming"
  UserAttributes.AttrWorkProp
    Info: AttrWorkProp: mess:"Wrong type" code:401; name:Mads Pedersen; date:2022-12-21
  UserAttributes.AttrWorkProp
    Info: AttrWorkProp: mess:"Overflow memory" code:506; name:Peter; date:2022-10-23

Attributes in methods:
  System.Runtime.CompilerServices.NullableContextAttribute
  System.Diagnostics.ConditionalAttribute
  System.ObsoleteAttribute
    Info: Obsolete: mess:"Don't use this version - it is obsolete" IsError:False; Id:;
  UserAttributes.FirstMyAttribute
    Info: FirstMyAttribute: "This method is obsolete.", "Topic: Method:DebugMessage"
  System.Runtime.CompilerServices.NullableContextAttribute
  System.Runtime.CompilerServices.IntrinsicAttribute
  UserAttributes.AttrWorkProp
    Info: AttrWorkProp: mess:"Method done well" code:102; name:Greg; date:2022-10-06
  System.Runtime.CompilerServices.NullableContextAttribute
  System.Runtime.CompilerServices.IntrinsicAttribute

```

