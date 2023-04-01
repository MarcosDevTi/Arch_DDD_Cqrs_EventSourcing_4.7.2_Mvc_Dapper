Functional Document - AssignEquipment
Overview
This document describes the AssignEquipment method of the LidController class in a .NET 6 application. The AssignEquipment method is used to assign equipment to a LID frequency in a database. This method receives a set of input parameters, performs some input validation, updates the database, and returns a message indicating the success or failure of the operation.

Inputs
The AssignEquipment method receives the following input parameters:

Name	Type	Description
NetworkKey	string	The network key to which the user belongs.
AccountNumber	string	The user's account number.
LID	string	The LID frequency to which the equipment will be assigned.
ApplicationType	int	The equipment's application type.
ModulationType	int	The equipment's modulation type.
TagNumber	string	The equipment's tag number.
EquipmentRecord	string	The equipment's record.
SerialNo	string	The equipment's serial number.
ErrorMessage	string	The error message returned by the method in case of failure.
Outputs
The AssignEquipment method returns a success message or an error message, depending on the result of the operation. The success message is a string indicating that the equipment was successfully assigned to the LID frequency. The error message is a string indicating what was wrong with the input inputs.

Behavior
The AssignEquipment method performs the following steps:

The method performs some input validation to ensure that the input inputs are valid. These validations include checking if the network key exists, if the account number exists, and if the LID frequency is already in use by another equipment.
The method retrieves the LID frequency identifier from the LID frequency input value.
The method retrieves the LID account identifier from the account number input value.
If the LID frequency is not in use, the method retrieves the appropriate subzone identifier for the LID frequency.
The method inserts a new equipment record into the database, using the application type, modulation type, tag number, equipment record, and serial number input values.
The method inserts a new frequency-equipment record into the database, using the LID frequency identifier and the equipment identifier.
The method updates the status of the LID frequency to "used".
If any error occurs during the execution of the method, the method updates the ErrorMessage variable with an appropriate error message.
If the operation is successful, the method returns a success message.
Non-functional Requirements
The AssignEquipment method should be secure against SQL injection attacks.
The AssignEquipment method should be able to handle multiple write operations simultaneously.
The AssignEquipment method should return a message indicating the success or failure of the operation in a timely manner.
