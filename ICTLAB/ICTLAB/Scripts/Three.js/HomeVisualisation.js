var scene = new THREE.Scene();

var camera = new THREE.PerspectiveCamera(120, window.innerWidth / window.innerHeight, 0.1, 3000);
camera.position.z = 5;


var renderer = new THREE.WebGLRenderer();
renderer.setSize(window.innerWidth, window.innerHeight);

document.body.appendChild(renderer.domElement);



var ambientLight = new THREE.AmbientLight(0xffffff, 0.5);
scene.add(ambientLight);

var pointLight = new THREE.PointLight(0xffffff, 0.5);
scene.add(pointLight);

var geometry = new THREE.BoxGeometry(1, 1, 1);
var material = new THREE.MeshLambertMaterial({ color: 0xF3FFE2 });
var cube = new THREE.Mesh(geometry, material);

scene.add(cube);



//loader = new THREE.ColladaLoader();
//loader.load('CHIBB.dae', function colladaReady(collada) {
//    player = collada.scene;
//    skin = collada.skins[0];
//    scene.add(player);
//});


//GETS ERROR: "Couldnt load chibb.dae"
//var loader = new THREE.ColladaLoader();

//loader.load('chibb.dae', function (result) {
//    object.scale.set(0.0025, 0.0025, 0.0025);
//    object.position.set(-2, 0.2, 0);
//    scene.add(result.scene);
//});




//var loader = new THREE.OBJLoader(); 
//loader.load(

// '/api/model/chibbHouse',

// function ( object ) {
//     scene.add( object );
// });


//var loader = new THREE.FileLoader();
//loader.load(
//    // resource URL
//    '/api/model/chibbHouse',

//    // Function when resource is loaded
//    function (data) {
//        // output the text to the console
//        //console.log(data);
//        scene.add(data);
//    },

//    // Function called when download progresses
//    function (xhr) {
//       // console.log((xhr.loaded / xhr.total * 100) + '% loaded');
//    },

//    // Function called when download errors
//    function (xhr) {
//        console.error('An error happened');
//    }
//);

loader = new THREE.JSONLoader();

loader.load("/api/model/chibbHouse", function (geometry) {
    var mesh = new THREE.Mesh(geometry, new THREE.MeshNormalMaterial());
    mesh.scale.set(10, 10, 10);
    mesh.position.y = 150;
    mesh.position.x = 0;
});


requestAnimationFrame(render);
function render() {
    cube.rotation.x += 0.1;
    cube.rotation.y += 0.1;

    
    renderer.render(scene, camera);
    requestAnimationFrame(render);
}
render();