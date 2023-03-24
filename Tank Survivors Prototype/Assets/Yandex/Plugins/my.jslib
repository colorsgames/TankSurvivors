mergeInto(LibraryManager.library, {
    SaveExtern: function(date){
        if(player != null)
        {
            var dateString = UTF8ToString(date);
            var myobj = JSON.parse(dateString);
            player.setData(myobj);
        }
        else
        {
            console.log("Player is null");
        }
    },

    LoadExtern: function(){
        if(player != null)
        {
            player.getData().then(_date => {
                const myJSON = JSON.stringify(_date);
                myGameInstance.SendMessage('Progress', 'SetProgressInfo', myJSON);
            });
        }
        else
        {
            console.log("Player is null");
        }
    },

    ShowAdv: function(){
        ysdk.adv.showFullscreenAdv({
    callbacks: {
        onOpen: () => {
            if(myGameInstance != null)
            {
                myGameInstance.SendMessage('SoundManager', 'ADVSoundStop');
            }
            console.log("___________OPEN___________");
        },
        onClose: function(wasShown) {
          // some action after close
            if(myGameInstance != null)
            {
                myGameInstance.SendMessage('SoundManager', 'ADVSoundReturn');
            }
          console.log("___________CLOSE___________");
          //ReturnSound();
        },
        onError: function(error) {
          // some action on error
        }
    }
})
    },

});