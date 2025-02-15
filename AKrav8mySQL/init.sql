
CREATE DATABASE IF NOT EXISTS Vehicledatabase;
USE Vehicledatabase;

CREATE TABLE IF NOT EXISTS cars (
    id INT AUTO_INCREMENT PRIMARY KEY,
    type VARCHAR(50) NOT NULL,   
    color VARCHAR(20) NOT NULL,  
    window_type VARCHAR(20) NOT NULL,  
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP
);


INSERT INTO cars (type, color, window_type) VALUES
('Car', 'Red', 'Clear'),
('Truck', 'Blue', 'Tinted'),
('Car', 'Green', 'Clear'),
('Truck', 'Yellow', 'Tinted');

--CREATE USER 'root'@'%' IDENTIFIED BY 'root';
--GRANT ALL PRIVILEGES ON yourdatabase.* TO 'root'@'%';
--FLUSH PRIVILEGES;