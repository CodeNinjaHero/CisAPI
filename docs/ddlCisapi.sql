-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema cisapidb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `cisapidb` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `cisapidb`;

-- -----------------------------------------------------
-- Table `cisapidb`.`categories`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cisapidb`.`categories` (
  `id` VARCHAR(36) NOT NULL,
  `name` VARCHAR(100) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `idx_categories_name` (`name` ASC) VISIBLE
) ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Table `cisapidb`.`users`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cisapidb`.`users` (
  `id` VARCHAR(36) NOT NULL,
  `name` VARCHAR(255) NULL DEFAULT NULL,
  `login` VARCHAR(255) NULL DEFAULT NULL,
  `password` VARCHAR(255) NULL DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `idx_users_login` (`login` ASC) VISIBLE
) ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Table `cisapidb`.`ideas`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cisapidb`.`ideas` (
  `id` VARCHAR(36) NOT NULL,
  `user_id` VARCHAR(36) NOT NULL,
  `title` VARCHAR(255) NOT NULL,
  `description` TEXT NOT NULL,
  `created_at` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  INDEX `idx_ideas_user_id` (`user_id` ASC) VISIBLE,
  INDEX `idx_ideas_status_created_at` (`created_at` DESC) VISIBLE,
  CONSTRAINT `ideas_ibfk_1`
    FOREIGN KEY (`user_id`)
    REFERENCES `cisapidb`.`users` (`id`)
    ON DELETE CASCADE
) ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Table `cisapidb`.`idea_categoria`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cisapidb`.`idea_categoria` (
  `idea_id` VARCHAR(36) NOT NULL,
  `category_id` VARCHAR(36) NOT NULL,
  PRIMARY KEY (`idea_id`, `category_id`),
  INDEX `idx_idea_categoria_idea_id` (`idea_id` ASC) VISIBLE,
  INDEX `idx_idea_categoria_category_id` (`category_id` ASC) VISIBLE,
  CONSTRAINT `idea_categoria_ibfk_1`
    FOREIGN KEY (`idea_id`)
    REFERENCES `cisapidb`.`ideas` (`id`)
    ON DELETE CASCADE,
  CONSTRAINT `idea_categoria_ibfk_2`
    FOREIGN KEY (`category_id`)
    REFERENCES `cisapidb`.`categories` (`id`)
    ON DELETE CASCADE
) ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Table `cisapidb`.`comments`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cisapidb`.`comments` (
  `id` VARCHAR(36) NOT NULL,
  `user_id` VARCHAR(36) NOT NULL,
  `idea_id` VARCHAR(36) NOT NULL,
  `content` TEXT NOT NULL,
  `created_at` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  INDEX `idx_comments_user_id` (`user_id` ASC) VISIBLE,
  INDEX `idx_comments_idea_id` (`idea_id` ASC) VISIBLE,
  INDEX `idx_comments_created_at` (`created_at` DESC) VISIBLE,
  CONSTRAINT `comments_ibfk_1`
    FOREIGN KEY (`user_id`)
    REFERENCES `cisapidb`.`users` (`id`)
    ON DELETE CASCADE,
  CONSTRAINT `comments_ibfk_2`
    FOREIGN KEY (`idea_id`)
    REFERENCES `cisapidb`.`ideas` (`id`)
    ON DELETE CASCADE
) ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

-- -----------------------------------------------------
-- Table `cisapidb`.`votes`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cisapidb`.`votes` (
  `id` VARCHAR(36) NOT NULL,
  `user_id` VARCHAR(36) NOT NULL,
  `idea_id` VARCHAR(36) NOT NULL,
  `vote_type` ENUM('up', 'down') NOT NULL,
  `created_at` TIMESTAMP NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `idx_votes_user_idea` (`user_id`, `idea_id`) VISIBLE,
  INDEX `idx_votes_idea_id` (`idea_id` ASC) VISIBLE,
  INDEX `idx_votes_created_at` (`created_at` DESC) VISIBLE,
  CONSTRAINT `votes_ibfk_1`
    FOREIGN KEY (`user_id`)
    REFERENCES `cisapidb`.`users` (`id`)
    ON DELETE CASCADE,
  CONSTRAINT `votes_ibfk_2`
    FOREIGN KEY (`idea_id`)
    REFERENCES `cisapidb`.`ideas` (`id`)
    ON DELETE CASCADE
) ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
