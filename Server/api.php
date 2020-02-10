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
    //Añadir name
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
        return json_encode(["token" => generateToken($datos['Username'])], 200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});

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

$app->get('/chat/contact', function ($request, $response, $args) {

    $datos = $request->getParsedBody();
    if(!checkToken($datos['Token'])){
        throw new Exception("Error de token");
    }
    $id = getUserId($datos['Token']);

    $sql = "SELECT userid \"contactid\",username,alias FROM user WHERE userid IN (SELECT contactid FROM contact WHERE userid = ".$id.")";
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
        return json_encode($data, 200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});

$app->post('/chat/contact/add', function ($request, $response, $args) {

    $datos = $request->getParsedBody();

    try {
        if(!isset($datos['Token'])) throw new Exception("No se envio el token",105);
        if(!checkToken($datos['Token'])){
            throw new Exception("Error token",105);
        }
        $id = getUserId($datos['Token']);
        if(!checkUserId($datos['ContactId'])) throw new Exception("Error contactid, Not Found",105);
        if($datos['ContactId']==$id) throw new Exception("Error contactid",105);

        $sql = "INSERT INTO contact (`userid`, `contactid`)VALUES(" . $id . "," . $datos['ContactId'] .  ")";


        $db = connectDB();
        $stmt = $db->query($sql);
        if ($stmt == False) {
            throw new Exception("Error de query en insertar user",105);
        }



        return json_encode([
            "ID" => $db->lastInsertId()],
            200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});

$app->delete('/chat/contact/delete', function ($request, $response, $args) {

    $datos = $request->getParsedBody();
    if(!checkToken($datos['Token'])){
        throw new Exception("Error token");
    }
    $id = getUserId($datos['Token']);
    if(!checkUserId($datos['ContactId'])) throw new Exception("Error contactid, Not Found");
    if($datos['ContactId']==$id) throw new Exception("Error contactid");

    $sql = "DELETE FROM contact WHERE userid=".$id." AND contactid=". $datos['ContactId'];

    try {
        $db = connectDB();
        $stmt = $db->query($sql);
        if ($stmt == False) {
            throw new Exception("Error de query en borrar contact");
        }



        return json_encode([
            "ID" => $db->lastInsertId()],
            200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});
$app->post('/chat/chat/add', function ($request, $response, $args) {

    $datos = $request->getParsedBody();
    if(!checkToken($datos['Token'])){
        throw new Exception("Error token");
    }
    $id = getUserId($datos['Token']);
    if(!checkUserId($datos['ContactId'])) throw new Exception("Error contactid, Not Found");
    if($datos['ContactId']==$id) throw new Exception("Error contactid");

    $sql = "INSERT INTO chat (`userid1`, `userid2`)VALUES(" . $id . "," . $datos['ContactId'] .  ")";

    try {
        $db = connectDB();
        $stmt = $db->query($sql);
        if ($stmt == False) {
            throw new Exception("Error de query en insertar chat");
        }



        return json_encode([
            "ID" => $db->lastInsertId()],
            200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});

$app->get('/chat/chat', function ($request, $response, $args) {

    $datos = $request->getParsedBody();
    if(!checkToken($datos['Token'])){
        throw new Exception("Error token");
    }
    $id = getUserId($datos['Token']);
    ////-------------------------------------
    /// --------------------------------------
    //PROBAR CON MUCHOS MAS DATOS
    $sql = "SELECT chatid,userid \"contactid\",username,alias FROM user INNER JOIN chat WHERE userid IN (SELECT userid1 FROM chat WHERE userid2= ".$id.") OR userid IN (SELECT userid2 FROM chat WHERE userid1= ".$id.")";
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);
        return json_encode($data, 200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});


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
/*
$app->post('/message/add', function ($request, $response, $args) {

    $datos = $request->getParsedBody();

    try {
        if(!isset($datos['Token'])) throw new Exception("No se envio el token",104);
        if(!isset($datos['Date'])) throw new Exception("No se envio la fecha",104);
        if(!isset($datos['Message'])) throw new Exception("No se envio el message",104);
        if(!isset($datos['Chatid'])) throw new Exception("No se envio el chatid",104);
        if(!checkToken($datos['Token'])){
            throw new Exception("Error token",104;
        }
        $id = getUserId($datos['Token']);
        $order = getMaxOrder($datos['Chatid'])+1;

        ///Comprobar que el usuario esta en ese chat
        $sql = "INSERT INTO message (`chatid`, `userid`,`message`,`date`,`order`)VALUES(".$datos['Chatid'].",".  $id . ",'" . $datos['Message'] .  "','". $datos['Date'] .  "',".$order.")";


        $db = connectDB();
        $stmt = $db->query($sql);
        if ($stmt == False) {
            throw new Exception("Error de query en insertar message",105);
        }



        return json_encode([
            "ID" => $db->lastInsertId()],
            200);
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }

});
function getMaxOrder($chatid){
    $sql = "SELECT max(order) 'Order' FROM chat WHERE chatid = ". $chatid;
    try {
        $db = connectDB();
        $stmt = $db->query($sql);

        $data = $stmt->fetchAll(PDO::FETCH_ASSOC);

        return $data[0]['Order'];
    } catch (PDOException $e) {
        return managerError($e);
    } catch (Exception $e) {
        return managerError($e);
    }
}
*/

$app->run();
