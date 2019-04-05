# knowledge base

## VueJs issues

### stopping vue from flashing the `{{ stuff }}`

- add v-cloak to elements you want cloaked. 
- NB! make sure your CSS style include link is loaded *before* the html that needs to be cloaked!
- `[v-cloak] { display: none !important;}` to your style.

### adding inline <style> breaks vuejs

- Adding a `<style>` element to a `*.cshtml` page results in `Uncaught RangeError: Invalid string length` error.
- **fix** : move style to style pages and add link  `<link href="~/Content/style-menus.css" rel="stylesheet" type="text/css" />`

### Apps are hard to un-cache or refresh

- refreshing the app, and re-rendering a vue component are not the same thing. btw, use a key for the latter.
- to 'uncache' the app, add `?ver=xx` or any cache buster to the script. e.g. `<script src="~/Scripts/src/MyApp.js?ver=1"></script>`
  (not sure if this is the correct way to do this, but I battled to find anything in google on the subject specifically to force uncaching the damned app code itself.)
- this is a F**** horrendous technique because everywhere where you reference the script will need to be updated,
   and you'll either have to do some stupid node precompile step and inject a token or variable, or it will 
   eventually get out of sync! i.e. pages 4,5, 6, 9 all on v10, and 7,8 on v9 and you didnt notice! Grrr, #FAIL !

### Packaging problems with nuspec

- checkout discussion here : https://stackoverflow.com/questions/14797525/differences-between-nuget-packing-a-csproj-vs-nuspec