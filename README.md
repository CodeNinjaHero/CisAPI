# ApiRest

## 📌 Setting Up the MySQL Database for `cisAPI`

Follow these steps to set up the MySQL database using Docker.

### 1️⃣ **Pull the MySQL Docker Image**

Download the latest MySQL image from Docker Hub:
`
```sh
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

- As **root** user:
```
docker exec -it cisAPI mysql -u root -p
```
- As **edwin** user:
```
docker exec -it cisAPI mysql -u edwin -p
```    

### 4️⃣ **Execute the Database Schema Script**

Once inside the MySQL CLI, run the database schema script:

copy and execute the script located at:  
📂 `docs/ddlCisapi.sql`

![[Pasted image 20250327000808.png]]

