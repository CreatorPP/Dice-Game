mergeInto(LibraryManager.library, {
  Hello: function () {
    window.alert('Hello world!');
  },
  SendPacket: function(id, str) {
    ReactUnityWebGL.SendPacket(id, Pointer_stringify(str));
  }
});