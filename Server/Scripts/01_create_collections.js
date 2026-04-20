db = db.getSiblingDB("DeviceDB");

if (db.getCollectionNames().includes("Devices")) {
    db.Devices.drop();
}
if (db.getCollectionNames().includes("Users")) {
    db.Users.drop();
}

db.createCollection("Devices");
db.createCollection("Users");

print("Collections created successfully.");