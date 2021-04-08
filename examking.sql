/*
 Navicat Premium Data Transfer

 Source Server         : 127.0.0.1
 Source Server Type    : MySQL
 Source Server Version : 80023
 Source Host           : localhost:3306
 Source Schema         : exam

 Target Server Type    : MySQL
 Target Server Version : 80023
 File Encoding         : 65001

 Date: 08/04/2021 21:22:16
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE IF EXISTS `__EFMigrationsHistory`;
CREATE TABLE `__EFMigrationsHistory` (
  `MigrationId` varchar(95) COLLATE utf8mb4_unicode_ci NOT NULL,
  `ProductVersion` varchar(32) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
BEGIN;
INSERT INTO `__EFMigrationsHistory` VALUES ('20201201170344_v1.0.0', '5.0.5');
COMMIT;

-- ----------------------------
-- Table structure for tb_admin
-- ----------------------------
DROP TABLE IF EXISTS `tb_admin`;
CREATE TABLE `tb_admin` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `username` varchar(30) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL COMMENT '账号',
  `password` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL COMMENT '密码',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `id` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='管理员表';

-- ----------------------------
-- Records of tb_admin
-- ----------------------------
BEGIN;
INSERT INTO `tb_admin` VALUES (1, 'admin', 'e10adc3949ba59abbe56e057f20f883e', '2020-11-18 13:15:38.000000');
COMMIT;

-- ----------------------------
-- Table structure for tb_chapter
-- ----------------------------
DROP TABLE IF EXISTS `tb_chapter`;
CREATE TABLE `tb_chapter` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `chapterName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '章节名称',
  `courseId` int NOT NULL COMMENT '课程ID',
  `desc` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '章节描述',
  PRIMARY KEY (`id`),
  UNIQUE KEY `chapter_id` (`id`),
  KEY `chapter_course_id` (`courseId`),
  CONSTRAINT `chapter_course_id` FOREIGN KEY (`courseId`) REFERENCES `tb_course` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='课程章节表';

-- ----------------------------
-- Records of tb_chapter
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_classes
-- ----------------------------
DROP TABLE IF EXISTS `tb_classes`;
CREATE TABLE `tb_classes` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `classesName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '班级名称',
  `DeptId` int NOT NULL COMMENT '系别ID',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `classes_id` (`id`),
  KEY `classes_dept_id` (`DeptId`),
  CONSTRAINT `classes_dept_id` FOREIGN KEY (`DeptId`) REFERENCES `tb_dept` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='班级表';

-- ----------------------------
-- Records of tb_classes
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_course
-- ----------------------------
DROP TABLE IF EXISTS `tb_course`;
CREATE TABLE `tb_course` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `courseName` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '课程名称',
  `DeptId` int NOT NULL COMMENT '系别ID',
  `teacherId` int NOT NULL COMMENT '教师ID',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `course_id` (`id`),
  KEY `course_dept_id` (`DeptId`),
  KEY `course_teacher_id` (`teacherId`),
  CONSTRAINT `course_dept_id` FOREIGN KEY (`DeptId`) REFERENCES `tb_dept` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `course_teacher_id` FOREIGN KEY (`teacherId`) REFERENCES `tb_teacher` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='课程表';

-- ----------------------------
-- Records of tb_course
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_courseclasses
-- ----------------------------
DROP TABLE IF EXISTS `tb_courseclasses`;
CREATE TABLE `tb_courseclasses` (
  `courseId` int NOT NULL COMMENT '课程ID',
  `classesId` int NOT NULL COMMENT '班级ID',
  PRIMARY KEY (`classesId`,`courseId`),
  KEY `courseclasses_classes_idx` (`classesId`),
  KEY `courseclasses_course_idx` (`courseId`),
  CONSTRAINT `courseclasses_classes_idx` FOREIGN KEY (`classesId`) REFERENCES `tb_classes` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `courseclasses_course_idx` FOREIGN KEY (`courseId`) REFERENCES `tb_course` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='课程班级关联表';

-- ----------------------------
-- Records of tb_courseclasses
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_dept
-- ----------------------------
DROP TABLE IF EXISTS `tb_dept`;
CREATE TABLE `tb_dept` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `deptName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '系别名称',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `dept_id` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='系别表';

-- ----------------------------
-- Records of tb_dept
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_exam
-- ----------------------------
DROP TABLE IF EXISTS `tb_exam`;
CREATE TABLE `tb_exam` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `examName` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '试卷名称',
  `courseId` int NOT NULL COMMENT '课程ID',
  `teacherId` int NOT NULL COMMENT '教师ID',
  `startTime` datetime(6) NOT NULL COMMENT '开始时间',
  `endTime` datetime(6) NOT NULL COMMENT '结束时间',
  `duration` int NOT NULL COMMENT '考试时长',
  `isEnable` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '启用状态',
  `isFinish` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '结束状态',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  `examScore` int NOT NULL COMMENT '试卷总分',
  `judgeScore` int NOT NULL COMMENT '是非题分值',
  `singleScore` int NOT NULL COMMENT '单选题分值',
  `selectScore` int NOT NULL COMMENT '多选题分值',
  PRIMARY KEY (`id`),
  UNIQUE KEY `exam_id` (`id`),
  KEY `exam_course_id` (`courseId`),
  KEY `exam_teacher_id` (`teacherId`),
  CONSTRAINT `exam_course_id` FOREIGN KEY (`courseId`) REFERENCES `tb_course` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `exam_teacher_id` FOREIGN KEY (`teacherId`) REFERENCES `tb_teacher` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='试卷表';

-- ----------------------------
-- Records of tb_exam
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_examclasses
-- ----------------------------
DROP TABLE IF EXISTS `tb_examclasses`;
CREATE TABLE `tb_examclasses` (
  `examId` int NOT NULL COMMENT '试卷ID',
  `classesId` int NOT NULL COMMENT '班级ID',
  PRIMARY KEY (`examId`,`classesId`),
  KEY `examclasses_classes_idx` (`classesId`),
  KEY `examclasses_exam_idx` (`examId`),
  CONSTRAINT `examclasses_classes_idx` FOREIGN KEY (`classesId`) REFERENCES `tb_classes` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `examclasses_exam_idx` FOREIGN KEY (`examId`) REFERENCES `tb_exam` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='试卷班级关联表';

-- ----------------------------
-- Records of tb_examclasses
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_examjobs
-- ----------------------------
DROP TABLE IF EXISTS `tb_examjobs`;
CREATE TABLE `tb_examjobs` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `examId` int NOT NULL COMMENT '考试ID',
  `status` int NOT NULL COMMENT '任务状态',
  PRIMARY KEY (`id`),
  UNIQUE KEY `examjobs_id` (`id`),
  KEY `examjobs_exam_id` (`examId`),
  CONSTRAINT `examjobs_exam_id` FOREIGN KEY (`examId`) REFERENCES `tb_exam` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='考试任务表';

-- ----------------------------
-- Records of tb_examjobs
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_examquestion
-- ----------------------------
DROP TABLE IF EXISTS `tb_examquestion`;
CREATE TABLE `tb_examquestion` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `questionType` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '题型',
  `examId` int NOT NULL COMMENT '试卷ID',
  `questionId` int NOT NULL COMMENT '题目ID',
  `score` int NOT NULL COMMENT '分数',
  PRIMARY KEY (`id`),
  UNIQUE KEY `examquestion_id` (`id`),
  KEY `IX_tb_examquestion_examId` (`examId`),
  CONSTRAINT `examquestion_exam_id` FOREIGN KEY (`examId`) REFERENCES `tb_exam` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='试卷题目关联表';

-- ----------------------------
-- Records of tb_examquestion
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_judge
-- ----------------------------
DROP TABLE IF EXISTS `tb_judge`;
CREATE TABLE `tb_judge` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `question` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '题目',
  `answer` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '答案',
  `courseId` int NOT NULL COMMENT '课程ID',
  `chapterId` int NOT NULL COMMENT '课程章节ID',
  `teacherId` int NOT NULL COMMENT '教师ID',
  `ideas` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '解题思路',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `judge_id` (`id`),
  KEY `judge_chapter_id` (`chapterId`),
  KEY `judge_source_id` (`courseId`),
  KEY `judge_teacher_id` (`teacherId`),
  CONSTRAINT `judge_chapter_id` FOREIGN KEY (`chapterId`) REFERENCES `tb_chapter` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `judge_source_id` FOREIGN KEY (`courseId`) REFERENCES `tb_course` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `judge_teacher_id` FOREIGN KEY (`teacherId`) REFERENCES `tb_teacher` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='是非题表';

-- ----------------------------
-- Records of tb_judge
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_select
-- ----------------------------
DROP TABLE IF EXISTS `tb_select`;
CREATE TABLE `tb_select` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `question` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '问题',
  `answer` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '答案',
  `isSingle` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '是否单选',
  `courseId` int NOT NULL COMMENT '课程ID',
  `chapterId` int NOT NULL COMMENT '课程章节ID',
  `teacherId` int NOT NULL COMMENT '教师ID',
  `optionA` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '选项A',
  `optionB` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '选项B',
  `optionC` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '选项C',
  `optionD` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '选项D',
  `ideas` varchar(300) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '解题思路',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `select_id` (`id`),
  KEY `select_chapter_id` (`chapterId`),
  KEY `select_source_id` (`courseId`),
  KEY `select_teacher_id` (`teacherId`),
  CONSTRAINT `select_chapter_id` FOREIGN KEY (`chapterId`) REFERENCES `tb_chapter` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `select_source_id` FOREIGN KEY (`courseId`) REFERENCES `tb_course` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `select_teacher_id` FOREIGN KEY (`teacherId`) REFERENCES `tb_teacher` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='选择题表';

-- ----------------------------
-- Records of tb_select
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_stuanswerdetail
-- ----------------------------
DROP TABLE IF EXISTS `tb_stuanswerdetail`;
CREATE TABLE `tb_stuanswerdetail` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `stuId` int NOT NULL COMMENT '学生ID',
  `examId` int NOT NULL COMMENT '试卷ID',
  `questionId` int NOT NULL COMMENT '题目ID',
  `questionType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci COMMENT '题型',
  `stuanswer` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '学生答案',
  `answer` varchar(100) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '正确答案',
  `isright` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '是否正确',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `answerdetail_stu_id_question_id` (`stuId`,`examId`,`questionId`),
  UNIQUE KEY `stuanseerdetail_id` (`id`),
  KEY `answerdetail_exam_id` (`examId`),
  KEY `answerdetail_examquestion_id` (`questionId`),
  KEY `answerdetail_stu_id` (`stuId`),
  CONSTRAINT `answerdetail_exam_id` FOREIGN KEY (`examId`) REFERENCES `tb_exam` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `answerdetail_examquestion_id` FOREIGN KEY (`questionId`) REFERENCES `tb_examquestion` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `answerdetail_stu_id` FOREIGN KEY (`stuId`) REFERENCES `tb_student` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='答题明细表';

-- ----------------------------
-- Records of tb_stuanswerdetail
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_student
-- ----------------------------
DROP TABLE IF EXISTS `tb_student`;
CREATE TABLE `tb_student` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `stuName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '姓名',
  `deptId` int NOT NULL COMMENT '系别ID',
  `classesId` int NOT NULL COMMENT '班级ID',
  `sex` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '性别',
  `stuNo` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '学号',
  `password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  `telphone` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '联系电话',
  `idCard` varchar(30) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '身份证号码',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `student_id` (`id`),
  KEY `student_classes_id` (`classesId`),
  KEY `student_dept_id` (`deptId`),
  CONSTRAINT `student_classes_id` FOREIGN KEY (`classesId`) REFERENCES `tb_classes` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `student_dept_id` FOREIGN KEY (`deptId`) REFERENCES `tb_dept` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='学生表';

-- ----------------------------
-- Records of tb_student
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_stuscore
-- ----------------------------
DROP TABLE IF EXISTS `tb_stuscore`;
CREATE TABLE `tb_stuscore` (
  `id` int unsigned NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `stuId` int NOT NULL COMMENT '学生ID',
  `courseId` int NOT NULL COMMENT '课程ID',
  `examId` int NOT NULL COMMENT '考试ID',
  `score` int NOT NULL COMMENT '分数',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `stuscore_id` (`id`),
  UNIQUE KEY `stuscore_stu_id_exam_id` (`stuId`,`examId`),
  KEY `stuscore_course_id` (`courseId`),
  KEY `stuscore_exam_id` (`examId`),
  KEY `stuscore_stu_id` (`stuId`),
  CONSTRAINT `stuscore_course_id` FOREIGN KEY (`courseId`) REFERENCES `tb_course` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `stuscore_exam_id` FOREIGN KEY (`examId`) REFERENCES `tb_exam` (`id`) ON DELETE RESTRICT,
  CONSTRAINT `stuscore_stu_id` FOREIGN KEY (`stuId`) REFERENCES `tb_student` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='学生成绩表';

-- ----------------------------
-- Records of tb_stuscore
-- ----------------------------
BEGIN;
COMMIT;

-- ----------------------------
-- Table structure for tb_teacher
-- ----------------------------
DROP TABLE IF EXISTS `tb_teacher`;
CREATE TABLE `tb_teacher` (
  `id` int NOT NULL AUTO_INCREMENT COMMENT 'ID',
  `teacherName` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '姓名',
  `sex` varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '性别',
  `telphone` varchar(25) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '联系电话',
  `teacherNo` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '教师编号',
  `password` varchar(50) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '密码',
  `DeptId` int NOT NULL COMMENT '系别ID',
  `idCard` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '身份证号',
  `createTime` datetime(6) NOT NULL DEFAULT CURRENT_TIMESTAMP(6) COMMENT '创建时间',
  PRIMARY KEY (`id`),
  UNIQUE KEY `teacher_id` (`id`),
  KEY `teacher_dept_id` (`DeptId`),
  CONSTRAINT `teacher_dept_id` FOREIGN KEY (`DeptId`) REFERENCES `tb_dept` (`id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='教师表';

-- ----------------------------
-- Records of tb_teacher
-- ----------------------------
BEGIN;
COMMIT;

SET FOREIGN_KEY_CHECKS = 1;
