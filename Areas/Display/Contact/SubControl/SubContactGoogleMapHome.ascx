<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubContactGoogleMapHome.ascx.cs" Inherits="Areas_Display_Contact_SubControl_SubContactGoogleMapHome" %>
<script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCgo4AsxbDxpabFhqsGycaolfcsyc-uEwM&callback=initMap&sensor=false"></script>
<%--<script src="https://maps.googleapis.com/maps/api/js?sensor=false&libraries=geometry,places&key=AIzaSyCgo4AsxbDxpabFhqsGycaolfcsyc-uEwM"></script>--%>
<section class="showroom full-section">
    <div id="map_canvas" class="map full-section">
        <%--<img src="images/map.jpg" width="100%">--%>
    </div>
    <div class="showroom-inner">
        <p class="showroom-tit">Showroom Janhome trên toàn quốc</p>
        <asp:Literal ID="ltrList" runat="server"></asp:Literal>
    </div>
</section>
<script type="text/javascript">
    var locations = [
        ['Location 1 Name', 'New York, NY', 'Location 1 URL'],
        ['Location 2 Name', 'Newark, NJ', 'Location 2 URL'],
        ['Location 3 Name', 'Philadelphia, PA', 'Location 3 URL']
    ];

    var geocoder;
    var map;
    var bounds = new google.maps.LatLngBounds();

    function initialize() {
        map = new google.maps.Map(
        document.getElementById("map_canvas"), {
            center: new google.maps.LatLng(37.4419, -122.1419),
            zoom: 13,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        });
        geocoder = new google.maps.Geocoder();

        for (i = 0; i < locations.length; i++) {


            geocodeAddress(locations, i);
        }
    }
    google.maps.event.addDomListener(window, "load", initialize);

    function geocodeAddress(locations, i) {
        var title = locations[i][0];
        var address = locations[i][1];
        var url = locations[i][2];
        geocoder.geocode({
            'address': locations[i][1]
        },

        function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var marker = new google.maps.Marker({
                    icon: 'http://maps.google.com/mapfiles/ms/icons/blue.png',
                    map: map,
                    position: results[0].geometry.location,
                    title: title,
                    animation: google.maps.Animation.DROP,
                    address: address,
                    url: url
                })
                infoWindow(marker, map, title, address, url);
                bounds.extend(marker.getPosition());
                map.fitBounds(bounds);
            } else {
                alert("geocode of " + address + " failed:" + status);
            }
        });
    }

    function infoWindow(marker, map, title, address, url) {
        google.maps.event.addListener(marker, 'click', function () {
            var html = "<div><h3>" + title + "</h3><p>" + address + "<br></div><a href='" + url + "'>View location</a></p></div>";
            iw = new google.maps.InfoWindow({
                content: html,
                maxWidth: 350
            });
            iw.open(map, marker);
        });
    }

    function createMarker(results) {
        var marker = new google.maps.Marker({
            icon: 'http://maps.google.com/mapfiles/ms/icons/blue.png',
            map: map,
            position: results[0].geometry.location,
            title: title,
            animation: google.maps.Animation.DROP,
            address: address,
            url: url
        })
        bounds.extend(marker.getPosition());
        map.fitBounds(bounds);
        infoWindow(marker, map, title, address, url);
        return marker;
    }


  </script>