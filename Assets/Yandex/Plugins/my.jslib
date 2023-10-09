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


  });


  