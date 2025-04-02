# ApiRest

## 📌 Setting Up the MySQL Database for `cisApi`
Follow these steps to set up the MySQL database using Docker.

### 1️⃣ **Pull the MySQL Docker Image**
Download the latest MySQL image from Docker Hub:

```
docker pull mysql:latest
```

### 2️⃣ **Run the MySQL Container**
Start a new MySQL container with the required credentials:

```
docker run -d --name cisAPI -e MYSQL_ROOT_PASSWORD=cisapi -e MYSQL_USER=edwin -e MYSQL_PASSWORD=edwin123 -e MYSQL_DATABASE=cisapidb -p 3307:3306 mysql
```

- `MYSQL_ROOT_PASSWORD=cisapi` → Sets the root password to **cisapi**.
- `MYSQL_USER=edwin` → Creates a new user **edwin**.
- `MYSQL_PASSWORD=edwin123` → Sets the password for user **edwin**.
- `MYSQL_DATABASE=cisapidb` → Creates the **cisapidb** database.
- `-p 3307:3306` → Maps MySQL’s port `3306` to `3307` on your local machine.
    

### 3️⃣ **Access the MySQL CLI**
To interact with the MySQL database inside the container, run one of the following commands:

```
docker exec -it cisAPI mysql -u root -p
```

or connect using the created user:

```
docker exec -it cisAPI mysql -u edwin -p
```

After entering the command, you will be prompted to enter the password (`edwin123`).

### 4️⃣ **Connect to MySQL from MySQL Workbench**

1. Open **MySQL Workbench**.
2. Click on `+` to add a new connection.
3. Enter the following details:
    - **Connection Name:** `cisApi`
    - **Hostname:** `127.0.0.1`
    - **Port:** `3307`
    - **Username:** `edwin`
    - **Password:** `edwin123` (Save in vault if preferred)
        
4. Click `Test Connection` to ensure it's working.
5. If successful, click `OK` to save the connection.

#### **Configure Connection String in** `**appsettings.json**`

If you don't use docker, ensure that your `appsettings.json` file includes the correct connection string:

```
"ConnectionStrings": {
    "DefaultConnection": "server=127.0.0.1;port=3307;database=cisapidb;user=edwin;password=edwin123;"
}
```

### 5️⃣ **Run Migrations (If Not Already Applied)**
Since the project already includes migrations, update the database schema using:

```
dotnet ef database update --project CisApi.DataAccess --startup-project CisApi
```

Other way is using Manager Console, select the proyect  `CisApi`
```
Update-Database
```

This command ensures the database is updated with the latest migration.

### ✅ **Everything is Set!**
Now your MySQL database is ready and configured for `cisApi` 🚀.

