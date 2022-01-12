# Why use this than that

## DATABASE ACCESS

### Code first approach

With latest versions of EF Core database first apporach is no longer suported, code first in only approach that can be used. This enables us to create migrations based on model changes, all migrations will be saved in source control and we can do upgrade and downgrade if needed. We don't need to save and run SQL scripts.

### Define database fields types and constraints

There is two approach to do so, using attrubutes on entity classes or using fluent sintax. It result the same behaviour but we choose fluent sintax because code is more clear, expecialy when we need to create custom attrubute, and entity classes stay clear, fluent implementation is in separate files.

