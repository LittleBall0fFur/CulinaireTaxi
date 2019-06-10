
var platform = new H.service.Platform({
    'app_id': 'MlKhwu5EkqSZONTwezB9',
    'app_code': 'jTvw0t1CFTzdvxpKytXhIw',
    'useHTTPS': true
});

var defaultLayers = platform.createDefaultLayers();

var map = new H.Map(
    document.getElementById('restaurants-map'),
    defaultLayers.normal.map,
    {}
);

var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(map));
var mapUI = H.ui.UI.createDefault(map, defaultLayers);

if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(SetGeo)
}

function SetGeo(position) {
    let x = position.coords.latitude;
    let y = position.coords.longitude;

    map.setCenter({ lat: x, lng: y });
    map.setZoom(14);
}

function CreateRestaurantMarker(restaurant) {

    var destinationMarker = new H.map.Marker({ lat: restaurant.x, lng: restaurant.y });
    destinationMarker.draggable = false;

    destinationMarker.setData(restaurant);

    destinationMarker.addEventListener(
        'tap',
        function (event) {
            var info =
                "<div class='restaurant-info'>" +
                "<h5>" + restaurant.name + "</h5>" +
                "<p>" + restaurant.description + "</p>" +
                "<button class='btn btn-primary' onclick='openOrderModal(" + restaurant.id + ")'>Place Reservation</button>" +
                "</div>";

            var infoBubble = new H.ui.InfoBubble(event.target.getPosition(), { content: info });

            mapUI.addBubble(infoBubble);
        },
        true
    );

    map.addObject(destinationMarker);
}
