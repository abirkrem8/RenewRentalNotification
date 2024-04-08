# Renew Rental Notification


## Description

The Renew Rental Notification System is an automation designed to streamline the process of identifying rental tenants who are moving out in two months, sending them personalized email notifications about their move-out date, and reminding them to renew their lease by a specific date. Additionally, it ensures efficient communication by providing the property manager with a comprehensive list of these tenants for follow-up purposes.

## Workflow
### 1. ETL
* First the automation will extract all of the Tenant Assignments from the MySQL database that fit the following criteria.
   * Expected Move Out Date between 60 and 67 days (2 months from now)
### 2. Send Emails To Tenants
* For each Tenant Assignment that fits that criteria, the automation will send an email to the tenant notifying them of the upcoming lease expiration. They will be directed to contact managment if they would like to renew their lease by a given deadline.
![tenant_email](https://github.com/abirkrem8/RenewRentalNotification/blob/master/Images/tenantemail.png?raw=true)
### 3. Send Email to Management
* Lastly, an email will be sent to management with an attached (.csv file) list of all of the tenants who have an upcoming lease expiration so they can follow up on renewing their lease.
![management_email](https://github.com/abirkrem8/RenewRentalNotification/blob/master/Images/managementemail.png?raw=true)
![management_attachment](https://github.com/abirkrem8/RenewRentalNotification/blob/master/Images/attachment.png?raw=true)

## Key Features
1. **MySQL Database Integration:** The system seamlessly integrates with the MySQL database housing all rental tenant information, allowing it to efficiently retrieve relevant data regarding upcoming move-outs.

3. **Data Processing:** Utilizing advanced querying techniques, the automation identifies all tenants scheduled to move out in two months. This includes parsing through lease agreements and move-out dates to accurately pinpoint the relevant individuals.
4. **Email Notification:** Once the target tenants are identified, the system automatically generates personalized email notifications. These emails contain essential information such as the confirmed move-out date and a polite reminder to consider renewing their lease. Personalization ensures that each tenant receives a tailored message, enhancing engagement and encouraging positive action.
5. **Property Manager Notification:** Simultaneously, the automation compiles a detailed list of the identified tenants along with pertinent details. This list is promptly forwarded to the property manager, enabling them to oversee the process and take necessary follow-up actions.
6. **Scheduling and Monitoring:** The automation can be scheduled to run at regular intervals, ensuring that upcoming move-outs are promptly identified and managed. Additionally, comprehensive logging and monitoring functionalities are integrated to track the execution of tasks and identify any potential issues.

## Benefits
1. **Efficiency:** By automating the process of identifying and notifying tenants, the system significantly reduces the time and effort required for manual data processing and communication.

3. **Proactive Communication:** Tenants receive timely notifications well in advance of their move-out dates, giving them ample time to consider their options and potentially renew their leases.
4. **Enhanced Management Oversight:** Property managers receive real-time updates on upcoming move-outs, enabling them to proactively address vacancies and ensure smooth transitions.
5. **Improved Tenant Satisfaction:** By providing proactive reminders and facilitating seamless communication, the automation contributes to a positive tenant experience and fosters tenant retention.  

## Getting Started

### Dependencies

#### Packages

* AutoMapper 13.0.1
* CsvHelper 31.0.3
* Dapper 2.1.35
* FluentValidation 11.9.0
* Microsoft.EntityFrameworkCore 7.0.17
* Microsoft.EntityFrameworkCore.Tools 7.0.17
* Microsoft.Extensions.Configuration 8.0.0
* Microsoft.Extensions.Hosting 8.0.0
* Pomelo.EntityFrameworkCore.MySql 7.0.0

#### Data
* MySQL Server 8.0

### Installing

1. Clone Github Repository git@github.com:abirkrem8/RenewRentalNotification.git
2. Set up Database (instructions below)
3. Modify development settings
   * Edit MySQL connection string in conf/appsettings.development.json
   * Add your email address to the "CCEmailAddress" field
   * Modify the "AttachmentFileOutputFolder" field to save the file locally on your machine
   * Generate an App Password for your email address and add it to the Email Settings in order to send emails from the program
  
#### Generate Data
Use the .sql files in the TestData/ folder to generate test data to run the program locally. Run the SQL in this order:
* CreateSchema.sql
* RentalProperties.sql
* RentalTenants.sql
* TenantAssignments.sql

### Executing program

* After the Database is populated, the program can be run by calling the executable file.
```
RenewRentalNotification.exe
```


## Authors

Allison Birkrem  
[Contact Email](allisonbirkrem@gmail.com)

## Version History

* 0.1 - 4/8/2024
    * Initial Release
