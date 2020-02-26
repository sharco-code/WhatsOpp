<?php
require './vendor/autoload.php';

$app = new Slim\App();

function connectDB()
{
    try {
        $usr = "root";
        $pass = "1234";
        $opciones = array(PDO::MYSQL_ATTR_INIT_COMMAND => "SET NAMES utf8");
        $conn = new PDO('mysql:host=localhost;dbname=chat', $usr, $pass, $opciones);
        return $conn;
    } catch (PDOException $e) {
        echo "ERROR: " . $e->getMessage();
    }
}

$app->post('/singin', function ($request, $response, $args) {

        $datos = $request->getParsedBody();
        try {
            if(!isset($datos['Username']))throw new Exception("Usuario no introducido",101);
            if(!isset($datos['Password']))throw new Exception("Password no introducida",101);
            if(!isset($datos['Email']))throw new Exception("Email no introducido",101);
            if(!isset($datos['Phone']))throw new Exception("Phone no introducido",101);
            if(!isset($datos['Name']))throw new Exception("Name no introducido",101);


            $sql = "INSERT INTO user (`username`, `password`,`email`,`phone`,`name`)VALUES('" . $datos['Username'] . "','" . $datos['Password'] . "','" . $datos['Email'] . "'," . $datos['Phone'] .",'".$datos['Name']. "')";

            $db = connectDB();
            $stmt = $db->query($sql);

            if($db->lastInsertId()==0){
                throw new Exception("Error al crear el usuario",101);
            }


            return json_encode(["token" => generateToken($datos['Username'])], 200);
        } catch (PDOException $e) {
            return managerError($e);
        } catch (Exception $e) {
            return managerError($e);
        }

    });

$app->post('/login', function ($request, $response, $args) {

    $datos = $request->getParsedBody();

    try {
        if(!isset($datos['Username']))throw new Exception("Usuario no introducido",102);
        if(!isset($datos['Password']))throw new Exception("Password no introducida",102);

        $sql = "SELECT * FROM user WHERE username = '". $datos['Username']."' AND password = '".$datos['Password']."'";
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
        if (sizeof($data) != 1){
            throw new Exception("No user/password encontrado",102);
        }
        $token = generateToken($datos['Username']);
        $id = getUserId($token);
        return json_encode([
            "Chats" => getChats($id),
            "Contacts"=> getContacts($id),
            "Profile" => getProfile($token)],200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});



$app->post('/contact', function ($request, $response, $args) {


    try {
        $datos = $request->getParsedBody();
        if(!checkToken($datos['Token']))throw new Exception("Error de token");

        $id = getUserId($datos['Token']);

        $sql = "SELECT userid \"contactid\",username,name FROM user WHERE userid IN (SELECT contactid FROM contact WHERE userid = ".$id.")";

        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return json_encode([
            "Contacts" => $data],200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});

$app->post('/contact/add', function ($request, $response, $args) {

    $datos = $request->getParsedBody();

    try {

        if(!isset($datos['Token'])) throw new Exception("No se envio el token",203);

        if(!checkToken($datos['Token'])){
            throw new Exception("Error token",203);
        }
        if(!isset($datos['Username'])) throw new Exception("No se envio el username",105);

        $id = getUserId($datos['Token']);

        $contactid = getIdByUsername($datos["Username"]);
        if(!$contactid) throw new Exception("No se encontro al usuario",105);
        if($contactid==$id) throw new Exception("Error contactid",105);

        if(!checkContact($id,$contactid)) throw new Exception("El contacto ya existe",105);
        $sql = "INSERT INTO contact (`userid`, `contactid`)VALUES(" . $id . "," . $contactid .  ")";

        $db = connectDB();
        $stmt = $db->query($sql);
        if ($stmt == False) {
            throw new Exception("Error de query en insertar contact",105);
        }



        return json_encode(getContact($datos["Username"]),  200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});

$app->post('/contact/delete', function ($request, $response, $args) {



    try {
        $datos = $request->getParsedBody();
        if(!checkToken($datos['Token'])) throw new Exception("Error token");
        $id = getUserId($datos['Token']);
        if(!checkUserId($datos['ContactId'])) throw new Exception("Error contactid, Not Found");
        if($datos['ContactId']==$id) throw new Exception("Error contactid");

        $sql = "DELETE FROM contact WHERE userid=".$id." AND contactid=". $datos['ContactId'];


        $db = connectDB();
        $stmt = $db->query($sql);
        if ($stmt == False) {
            throw new Exception("Error de query en borrar contact");
        }



        return json_encode(
            $db->lastInsertId(),
            200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});
$app->post('/chat/add', function ($request, $response, $args) {



    try {

        $datos = $request->getParsedBody();
        if(!checkToken($datos['Token'])){
            throw new Exception("Error token");
        }
        $id = getUserId($datos['Token']);
        $contactid = $datos['ContactID'];

        if(!checkChat($id,$contactid)) throw new Exception("Chat ya existe");

        $sql = "INSERT INTO chat (`userid1`, `userid2`)VALUES(" . $id . "," . $contactid .  ")";

        $db = connectDB();
        $stmt = $db->query($sql);
        if ($stmt == False) {
            throw new Exception("Error de query en insertar chat");
        }


        return json_encode(
            getChat($db->lastInsertId(),$id),
            200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});

$app->post('/chats', function ($request, $response, $args) {

    try {
        $datos = $request->getParsedBody();
        if(!checkToken($datos['Token']))  throw new Exception("Error token");

        $id = getUserId($datos['Token']);
        $sql = "SELECT chatid, if(userid1 = ".$id.",userid2,userid1 )\"ContactId\" , username,name FROM chat INNER JOIN user ON (userid1 = userid AND userid2 = ".$id.") OR (userid2 = userid AND userid1 = ".$id.")";

        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
        return json_encode(
            ["Chats" => $data],
            200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});

$app->post('/message/add', function ($request, $response, $args) {

    $datos = $request->getParsedBody();


    try {

        if(!isset($datos['Token'])) throw new Exception("No se envio el token",104);


        if(!isset($datos['Date'])) throw new Exception("No se envio la fecha",104);
        if(!isset($datos['Text'])) throw new Exception("No se envio el texto",104);
        if(!isset($datos['ChatID'])) throw new Exception("No se envio el chatid",104);
        if(!checkToken($datos['Token'])){
            throw new Exception("Error token",104);
        }

        $id = getUserId($datos['Token']);

        $order = getMaxOrder($datos['ChatID'])+1;


        $sql = "INSERT INTO message (`chatid`, `userid`,`message`,`date`,`order`)VALUES(".$datos['ChatID'].",".  $id . ",'" . $datos['Text'] .  "','". $datos['Date'] .  "',".$order.")";


        $db = connectDB();
        $stmt = $db->query($sql);
        if ($stmt == False) {
            throw new Exception("Error de query en insertar message",105);
        }



        return json_encode(
             getMessage($db->lastInsertId()),
            200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }


});

$app->post('/messages', function ($request, $response, $args) {

    $datos = $request->getParsedBody();


    try {

        if(!isset($datos['Token'])) throw new Exception("No se envio el token",104);
        if(!isset($datos['ChatID'])) throw new Exception("No se envio el chatid",104);
        if(!checkToken($datos['Token'])){
            throw new Exception("Error token",104);
        }

        $id = getUserId($datos['Token']);



        $sql = "SELECT chatid,messageid,message.userid,message.order,date,message.message 'Text',user.name 'Name' FROM message NATURAL JOIN user WHERE chatid = ".$datos['ChatID']." and message.userid != ".$id." AND message.read != 1";

        $db = connectDB();
        $stmt = $db->query($sql);
        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        foreach ($data as $fila){
            markAsRead($fila['messageid']);
        }

        return json_encode([
            "Messages"=>$data],
            200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }


});
function getMaxOrder($chatid){
    $sql = "SELECT IFNULL(max(message.order),0)'Order' FROM message WHERE chatid = ". $chatid;
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);;
        return $data[0]['Order'];
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}



function markAsRead($messageid)
{
    $sql = "UPDATE message SET message.read = 1 WHERE messageid = " . $messageid;
    try {
        $db = connectDB();
        $db->query($sql);

    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);


    }
}

function getUserId($token){
    $sql = "SELECT * FROM user WHERE token = '". $token."'";
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return $data[0]['userid'];
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}

function getIdByUsername($username){
    $sql = "SELECT * FROM user WHERE username = '". $username."'";
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
        if(sizeof($data) == 0)return;
        return $data[0]['userid'];
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}

function getContact($username){
    $sql = "SELECT userid 'ContactId',username,name FROM user WHERE username = '". $username."'";
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
        return $data[0];
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}

function checkUserId($id){
    $sql = "SELECT * FROM user WHERE userid = ". $id;
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return sizeof($data)== 1;
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}
function managerError(Exception $e){
    return json_encode(["Message"=>$e->getMessage(),"Code"=>$e->getCode()],200);

}


function getProfile($token){
    $sql = "SELECT username,name,phone,email,token FROM user WHERE token = '". $token."'";
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return $data[0];
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}

function getContacts($id){
    $sql = "SELECT userid \"contactid\",username,name FROM user WHERE userid IN (SELECT contactid FROM contact WHERE userid = ".$id.")";
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return $data;
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}

function getChats($id){
    $sql = "SELECT chatid, if(userid1 = ".$id.",userid2,userid1 )\"ContactId\" , username,name FROM chat INNER JOIN user ON (userid1 = userid AND userid2 = ".$id.") OR (userid2 = userid AND userid1 = ".$id.")";

    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return $data;
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}

function getChat($chatid,$id){
    $sql = "SELECT chatid, if(userid1 = ".$id.",userid2,userid1 )\"ContactId\" , username,name FROM chat INNER JOIN user ON (userid1 = userid AND userid2 = ".$id.") OR (userid2 = userid AND userid1 = ".$id.") WHERE chatid = ".$chatid;

    try {
        $db = connectDB();
        $stmt = $db->query($sql);
        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
        return $data[0];
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}
function generateToken($username){
    $characters = '0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ';
    $charactersLength = strlen($characters);
    $randomString = '';
    for ($i = 0; $i < 32; $i++) {
        $randomString .= $characters[rand(0, $charactersLength - 1)];
    }

    $sql = "UPDATE user SET token = '".$randomString."' WHERE username='".$username."'";

    try {
        $db = connectDB();
        $stmt = $db->query($sql);



    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }



    return $randomString;
}
function checkToken($token){
    $sql = "SELECT * FROM user WHERE token = '". $token."'";
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return sizeof($data)== 1;
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

}

function checkChat($id,$contactid){
    $sql = "SELECT * FROM chat WHERE  (userid1 = ".$id." AND userid2 = ".$contactid.") OR (userid1 = ".$contactid." AND userid2 = ".$id.")";
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
        return sizeof($data) == 0;
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

}

function checkContact($id,$contactid){

    $sql = "SELECT * FROM contact WHERE  userid = ".$id." AND contactid = ".$contactid;

    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return sizeof($data) == 0;
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

}

function getMessage($id){

    $sql = "SELECT messageid,chatid,message.order,message.date,message.message 'Text',user.name 'Name' FROM message NATURAL JOIN user WHERE  messageid = ".$id;

    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return $data[0];
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

}


$app->run();
