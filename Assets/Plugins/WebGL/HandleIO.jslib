//put this in Plugins/WebGL folder for save
 var HandleIO = {
     WindowAlert : function(message)
     {
         window.alert(Pointer_stringify(message));
     },
     SyncFiles : function()
     {
         FS.syncfs(false,function (err) {
             // handle callback
         });
     }
 };
 
 mergeInto(LibraryManager.library, HandleIO);