//SVG images
var house = '<svg  width="24" height="24" xmlns="http://www.w3.org/2000/svg"> <g><path d="M990,446.2L990,446.2L508.6,91.5L10,444.8L77.8,510l58-41.4v439.9h745.1V480.6l42.4,31.8L990,446.2z M802.5,425.7v408.5H214.2V425.7h-18.3l313.8-224l298.1,224H802.5z"/></g> </svg>';
var wineglass = '  <svg x="0px" y="0px" viewBox="0 0 495.281 495.281" style="enable-background:new 0 0 495.281 495.281;" xml:space="preserve"> <path id="oversized-wine-glass-2" style="fill:#000100;" d="M264.082,433.09V263.641c79.75-9.133,136.491-75.908,136.491-152.08 c0-28.795-7.996-64.61-21.854-99.33C375.775,4.847,368.617,0,360.671,0H134.481c-7.951,0-15.108,4.847-18.049,12.232 C103.92,43.581,94.199,80.534,94.728,114.633l0,0c0,0.032,0.009,0.064,0.009,0.095c1.54,75.125,57.814,140.143,136.323,148.913 V433.09c-51.979,4.254-91.823,27.358-91.823,38.974c0,12.834,48.501,23.217,108.341,23.217c59.839,0,108.345-10.383,108.345-23.217 C355.923,460.448,316.066,437.339,264.082,433.09z M143.823,33.021h207.502c10.361,28.255,16.227,56.47,16.227,78.541 c0,5.359-0.476,10.61-1.16,15.792c-10.324-2.573-21.025-2.913-32.504,1.173c-29.977,10.673-49.411,17.509-79.909,5.549 c-30.325-11.901-49.488-65.96-85.852-61.847c-14.447,1.626-26.842,6.378-38.031,12.979 C132.692,68.828,137.222,51.028,143.823,33.021z"/> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> <g> </g> </svg>'
var platform = new H.service.Platform({
    'app_id': 'MlKhwu5EkqSZONTwezB9',
    'app_code': 'jTvw0t1CFTzdvxpKytXhIw',
    'useHTTPS': true
});
var defaultLayers = platform.createDefaultLayers();
var x = 52.5;
var y = 13.4;
var homeMarker;
var destinationMarker;
if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(SetGeo)
}
var map = new H.Map(
    document.getElementById('map'),
    defaultLayers.normal.map,
    {
    }
);
console.log(map.center);
function SetGeo(position) {
    console.log(position);
    x = position.coords.latitude;
    y = position.coords.longitude;
    homeMarker.setPosition({ lat: x, lng: y });
    destinationMarker.setPosition({ lat: x, lng: y + 0.05 })
    map.setCenter({ lat: x, lng: y });
    map.setZoom(14);
}
var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(map));
var ui = H.ui.UI.createDefault(map, defaultLayers);
//Setup markers
CreateHomeMarker();
CreateDestinationMarker();
var homeIcon = new H.map.Icon(house);
function CreateHomeMarker() {
    homeMarker = new H.map.Marker({ lat: 42.35805, lng: -71.0636 }, { icon: homeIcon });
    homeMarker.draggable = true;
    map.addObject(homeMarker);
    map.addEventListener('dragstart', function (ev) {
        var target = ev.target;
        if (target instanceof H.map.Marker) {
            behavior.disable();
        }
    }, false);

    map.addEventListener('dragend', function (ev) {
        var target = ev.target;
        if (target instanceof mapsjs.map.Marker) {
            behavior.enable();
        }
    }, false);
    map.addEventListener('drag', function (ev) {
        var target = ev.target;
        pointer = ev.currentPointer;
        if (target instanceof mapsjs.map.Marker) {
            target.setPosition(map.screenToGeo(pointer.viewportX, pointer.viewportY));
        }
    }, false);
}
var wineGlassIcon = new H.map.Icon(wineglass);
function CreateDestinationMarker() {
    destinationMarker = new H.map.Marker({ lat: 42.35805, lng: -71.0636 }, { icon: wineGlassIcon });
    destinationMarker.draggable = true;
    map.addObject(destinationMarker);
    map.addEventListener('dragstart', function (ev) {
        var target = ev.target;
        if (target instanceof H.map.Marker) {
            behavior.disable();
        }
    }, false);
    map.addEventListener('dragend', function (ev) {
        var target = ev.target;
        if (target instanceof mapsjs.map.Marker) {
            behavior.enable();
        }
    }, false);
    map.addEventListener('drag', function (ev) {
        var target = ev.target;
        pointer = ev.currentPointer;
        if (target instanceof mapsjs.map.Marker) {
            target.setPosition(map.screenToGeo(pointer.viewportX, pointer.viewportY));
            console.log(target);
        }
    }, false);
}