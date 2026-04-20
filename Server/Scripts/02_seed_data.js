db = db.getSiblingDB("DeviceDB");

db.Users.deleteMany({});
db.Devices.deleteMany({});

db.Users.insertMany([
    { name: "Andrei Motoc", role: "Developer", location: "Oradea" },
    { name: "Maria Ionescu", role: "QA Engineer", location: "Cluj-Napoca" },
    { name: "Alexandru Pop", role: "DevOps", location: "Bucuresti" }
]);

const user1 = db.Users.findOne({ name: "Andrei Motoc" });
const user2 = db.Users.findOne({ name: "Maria Ionescu" });

db.Devices.insertMany([
    {
        name: "iPhone 17 Pro",
        manufacturer: "Apple",
        type: "phone",
        operatingSystem: "iOS",
        osVersion: "18",
        processor: "A19 Pro",
        ram: 12,
        description: "High-performance Apple smartphone.",
        assignedUserId: user1._id.toString()
    },
    {
        name: "Samsung Galaxy S24",
        manufacturer: "Samsung",
        type: "phone",
        operatingSystem: "Android",
        osVersion: "14",
        processor: "Snapdragon 8 Gen 3",
        ram: 8,
        description: "Flagship Android smartphone.",
        assignedUserId: user2._id.toString()
    },
    {
        name: "iPad Pro 13",
        manufacturer: "Apple",
        type: "tablet",
        operatingSystem: "iPadOS",
        osVersion: "18",
        processor: "M4",
        ram: 16,
        description: "Professional Apple tablet.",
        assignedUserId: null
    }
]);

print("Seed data inserted successfully.");