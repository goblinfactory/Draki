# Alan's Backlog & random thoughts - notes

### TODO (to investigate)

* Strip out and remove as much code as I can.
  - do we really need runtime assembly loading? see FluentSession
  - do we need Expressions?
  - do we need the whole API?
* change the default for Text entry to be without events, this will make the tests more robust and faster. If you want to specifically trigger the events, then the default should require you to specify WithEvents() or something like that. Rationale is to stay away from SendKeys that can send keystrokes to windows outside of the browser. This is brittle and when it breaks it's NAAASTY!

### Housekeeping

* look through code for all instances where Global settings have been changed as part of a test, and add in `try {} finally` code blocks to reset settings. 

* Aaargh, the fact that I'm having problem isolating these global settings, is a bigger concern that this should be dealt with and the only way that global settings should be able to be overridden should be as a parameter to the currently running test! Otherwise we can have all sorts of testing issues.
* no point in fixing this problem by forcing the test runner to run single threaded, since we can't make that a pre-requisite for the users of the library. The same problem we have testing our library is the same problem that users will have with their own code, so this must be fixed! Mitigation is to at the very least we should use `save, try { change } finally { restore }` pattern. ***this is not a good fix, but it will be better than what we have at the moment!*** 
* I would give up the fluent syntax in preference for fixing the above issue! if it came down to a choice?

### issue : fluent syntax and chaining in a test dsl

* chaining is a PAIN IN THE ASS when something goes wrong and you need to debug into a massively chained block of code. First thing I usually have to do is undo the chaining so that I can single step through the code. It's important to remember that this is **not** a functional library returning a solution based on a function that works with nice immutable data. Every single chained command mutates the state of the entire system, i.e. the web app. So (just in case anyone want's to say that DSL's promote functional code, and less side effects, in this instance that's not true.)
