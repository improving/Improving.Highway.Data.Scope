# Improving.Highway.Data.Scope
A scoped DataContext implementation for Highway.Data and Entity Framework

The idea for this code comes from http://mehei.me.  We were wanting to add scopes to the DbContext, and he had already done the work.
He has a great blog post about it here. [Managing DbContext the right way with Entity Framework 6: an in-depth guide](http://mehdi.me/ambient-dbcontext-in-ef6/)

I adapted the DbContextScope to work with HighwayData.  Since I am often using multiple databases in the same application, 
I integrated DbContexScope into Highways DomainRepository.

Assuming you have a DomainRepository

    private readonly IRepository<ITestDomain> _repository;



##Read Write Scope

      using (var scope = _repository.Scopes.Create())  
      {
          var verse = Map(new Verse(), message.Resource);
          verse.Created = _now;

           _repository.Context.Add(verse);

          var result = new VerseData();
          await scope.SaveChangesAsync((s, c) =>
          {
              result.Id         = verse.Id;
              result.RowVersion = verse.RowVersion;
          });

          return result;
      }


##ReadOnly Scope
        
        using (_repository.Scopes.CreateReadOnly())
        {
             var data = (await _repository.FindAsync(new GetBooksById(message.Ids)))
               ?.Select(x => Map(new BookData(), x))
               .ToArray();

             return new BookResult
             {
                 Books = data
             };
        }

