var lat, lng;
var closeModal = document.getElementsByClassName('close')[0];
var uploadModal = document.getElementById('myModal');
var uploadButton = document.getElementById('modalButton')

function loadMarkers(map) {
  $.getJSON('http://localhost:5000/get_markers', function(markers) {
    for (var marker in markers) {
      var lat = parseFloat(marker.split(',')[0]);
      var lng = parseFloat(marker.split(',')[1]);
      var marker = new google.maps.Marker({
        position: {lat: lat, lng: lng},
        map: map,
      });
    }
  });
}

function initMap() {
  var map = new google.maps.Map(document.getElementById('map'), {
    center: {lat: 37.87200, lng: -122.2585}, //MIT = 42.3601° N, 71.0942 W // Cal = 37.8719° N, 122.2585° W
    zoom: 18
  });

  var drawingManager = new google.maps.drawing.DrawingManager({
    drawingMode: google.maps.drawing.OverlayType.MARKER,
    drawingControl: true,
    drawingControlOptions: {
      position: google.maps.ControlPosition.TOP_CENTER,
      drawingModes: ['marker']
    },
    markerOptions: {},//icon: 'https://developers.google.com/maps/documentation/javascript/examples/full/images/beachflag.png'},
    circleOptions: {
      fillColor: '#ffff00',
      fillOpacity: 1,
      strokeWeight: 5,
      clickable: true,
      editable: true,
      zIndex: 1
    }
  });

  loadMarkers(map); 

  drawingManager.setMap(map);

  google.maps.event.addListener(drawingManager, 'markercomplete', function(marker) {
      drawingManager.setDrawingMode(null);
      google.maps.event.addListener(marker, 'click', function() {
        document.getElementById('myModal').style.display = "block";
        lat = marker.getPosition().lat();
        lng = marker.getPosition().lng();
      });
  });
}

uploadButton.onclick = function() {
    uploadModal.style.display = "none";
};

window.onclick = function(event) {
    var path = lat + '_' + lng;
    document.getElementById("coord").value = path;

    if (event.target == closeModal) {
        uploadModal.style.display = "none";
    }
};

