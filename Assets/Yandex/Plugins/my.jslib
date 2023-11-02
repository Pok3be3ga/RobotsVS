mergeInto(LibraryManager.library, {

  	Hello: function () {
    	window.alert("Hello, world!");
    	console.log("Hello World");
  	},

  	GiveMePlayerData: function () {
  		myGameInstance.SendMessege("Yandex", "SetName", player.getName());
  		myGameInstance.SendMessege("Yandex", "SetPhoto", player.getPhoto("medium"));
  	},
  	SaveExtern: function(date){
  		var dateString = UTF8ToString(date);
		var myobj = JSON.parse(dateString);
		player.setData(myobj);

  	},
  	LoadExtern: function(){
  		player.getData().then(_date => {
				const myJSON = JSON.stringify(_date);
				myGameInstance.SendMessage('Progress', 'SetProgressData', myJSON);
				  });
  	},
  	AddCoinsExtern : function(){
  		ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessege("CoinCounter", "MultiplyCoins");
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})

  	}


  });


  