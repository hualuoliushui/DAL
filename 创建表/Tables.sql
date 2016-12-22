drop database testDal;
create database testDal;
use testDal;
create table person(
	personID Int(32) NOT NULL,
	personName varchar(255) NOT NULL UNIQUE,
	personPassword varchar(255) NOT NULL,
	personDepartment varchar(255) NOT NULL,
	personJob varchar(255) NOT NULL,
	personDescription varchar(255) default NULL,
	personState int(32) DEFAULT 0,
	#指示该用户是否为超级管理员，默认为false
	isAdmin boolean default false,
	CONSTRAINT PK_PERSONID primary key(personID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table role(
	roleID Int(32) NOT NULL,
	roleName varchar(255) NOT NULL UNIQUE,
	#该角色是否必需，即业务不可缺少，默认为false
	isIntegrant boolean DEFAULT false,
	CONSTRAINT PK_ROLEID primary key(roleID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table person_role(
	person_roleID Int(32) NOT NULL,
	personID Int(32) NOT NULL,
	roleID Int(32) NOT NULL,
	CONSTRAINT PK_PERSON_ROLEID primary key(person_roleID),
	CONSTRAINT FK_PERSON_ROLE_PERSONID foreign key(personID) references person(personID),
	CONSTRAINT FK_PERSON_ROLE_ROLEID foreign key(roleID) references role(roleID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table permission(
	permissionID Int(32) NOT NULL,
	permissionName varchar(255) NOT NULL UNIQUE,
	permissionDescription varchar(255) NOT NULL UNIQUE,
	CONSTRAINT PK_PERMISSIONID primary key(permissionID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table role_permission(
	role_permissionID Int(32) NOT NULL,
	roleID Int(32) NOT NULL,
	permissionID Int(32) NOT NULL,
	CONSTRAINT PK_ROLE_PERMISSIONID primary key(role_permissionID),
	CONSTRAINT FK_ROLE_PERMISSION_ROLEID foreign key(roleID) references role(roleID),
	CONSTRAINT FK_ROLE_PERMISSION_PERMISSIONID foreign key(permissionID) references permission(permissionID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table device(
	deviceID Int(32) NOT NULL,
	IMEI varchar(255) NOT NULL UNIQUE,
	deviceIndex Int(32) NOT NULL UNIQUE,
	deviceState Int(32) DEFAULT 0,
	CONSTRAINT PK_DEVICEID primary key(deviceID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table meetingPlace(
	meetingPlaceID Int(32) NOT NULL,
	meetingPlaceName varchar(255) NOT NULL UNIQUE,
	meetingPlaceCapacity varchar(255) DEFAULT 0,
	meetingPlaceState Int(32) DEFAULT 0,
	CONSTRAINT PK_MEETINGPLACEID primary key(meetingPlaceID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table meeting(
	meetingID Int(32) NOT NULL,
	meetingName varchar(255) NOT NULL UNIQUE,
	meetingSummary varchar(255) DEFAULT "",
	meetingDuration Int(32) DEFAULT 0,
	meetingToStartTime DateTime DEFAULT now(),
	meetingStartedTime DateTime DEFAULT now(),
	#默认会议：未开启==1
	meetingStatus Int(32) DEFAULT 1,
	#默认会议无更新：==0
	delegateUpdateStatus Int(32) DEFAULT 0,
	agendaUpdateStatus Int(32) DEFAULT 0,
	fileUpdateStatus Int(32) DEFAULT 0,
	voteUpdateStatus Int(32) DEFAULT 0,
	meetingPlaceID Int(32) NOT NULL,
	personID Int(32) NOT NULL,
	CONSTRAINT PK_MEETINGID primary key(meetingID),
	CONSTRAINT FK_MEETING_MEETINGPLACEID foreign key(meetingPlaceID) references meetingPlace(meetingPlaceID),
	CONSTRAINT FK_MEETING_PERSONID foreign key(personID) references person(personID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table delegate(
	delegateID Int(32) NOT NULL,
	isSignIn Boolean DEFAULT FALSE,
	personMeetingRole Int(32) DEFAULT 0,
	deviceID Int(32) NOT NULL,
	meetingID Int(32) NOT NULL,
	personID Int(32) NOT NULL,
	isUpdate Boolean Default false,
	CONSTRAINT PK_DELEGATEID primary key(delegateID),
	CONSTRAINT FK_DELEGATE_DEVICEID foreign key(deviceID) references device(deviceID),
	CONSTRAINT FK_DELEGATE_MEETINGID foreign key(meetingID) references meeting(meetingID),
	CONSTRAINT FD_DELEGATE_PERSONID foreign key(personID) references person(personID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table agenda(
	agendaID Int(32) NOT NULL,
	agendaName varchar(255) NOT NULL,
	agendaIndex Int(32) NOT NULL,
	agendaDuration Int(32) DEFAULT 0,
	meetingID Int(32) NOT NULL,
	personID Int(32) NOT NULL,
	isUpdate Boolean Default false,
	CONSTRAINT PK_AGENDAID primary key(agendaID),
	CONSTRAINT FK_AGENDA_MEETINGID foreign key(meetingID) references meeting(meetingID),
	CONSTRAINT FK_AGENDA_PERSONID foreign key(personID) references person(personID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table file(
	fileID Int(32) NOT NULL,
	fileName varchar(255) NOT NULL,
	fileIndex Int(32) NOT NULL,
	fileSize Int(32) NOT NULL,
	filePath varchar(255) NOT NULL UNIQUE,
	agendaID Int(32) NOT NULL,
	isUpdate Boolean Default false,
	CONSTRAINT PK_FILEID primary key(fileID),
	CONSTRAINT FK_FILE_AGENDAID foreign key(agendaID) references agenda(agendaID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table vote(
	voteID Int(32) NOT NULL,
	voteName varchar(255) NOT NULL,
	voteIndex Int(32) NOT NULL,
	voteDescription varchar(255) DEFAULT "",
	voteType Int(32) NOT NULL,
	voteStatus Int(32) DEFAULT 1,
	agendaID Int(32) NOT NULL,
	isUpdate Boolean Default false,
	CONSTRAINT PK_VOTEID primary key(voteID),
	CONSTRAINT FK_VOTE_AGENDAID foreign key(agendaID) references agenda(agendaID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table voteOption(
	voteOptionID Int(32) NOT NULL,
	voteOptionName varchar(255) NOT NULL,
	voteOptionIndex Int(32) NOT NULL,
	voteID Int(32) NOT NULL,
	CONSTRAINT PK_VOTEOPTIONID primary key(voteOptionID),
	CONSTRAINT FK_VOTEOPTION_VOTEID foreign key(voteID) references vote(voteID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;

create table voteOptionPersonResult(
	voteOptionPersonResultID Int(32) NOT NULL,
	voteOptionID Int(32) NOT NULL,
	personID Int(32) NOT NULL,
	#新增VoteID外键，便于查询
	CONSTRAINT PK_VOTEOPTIONPERSONRESULTID primary key(voteOptionPersonResultID),
	CONSTRAINT FK_VOTEOPTIONPERSONRESULT_VOTEOPTIONID foreign key(voteOptionID) references voteOption(voteOptionID),
	CONSTRAINT FK_VOTEOPTIONPERSONRESULT_PERSONID foreign key(personID) references person(personID)
)CHARACTER SET utf8 COLLATE utf8_general_ci;
