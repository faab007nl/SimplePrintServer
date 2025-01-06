# Simple Print Server

A simple print server that allows you to print to a local printer using a rest API.


# Api Endpoints

### Error Response
A error response will always have a `success` field set to `false` and a `message` field with a error message.
```json
{
    "success": false,
    "message": "Error message"
}
```

## Status
`GET /`  
Returns the status of the print server

### Response
```json
{
    "success": true|false,
    "message": "Server is running"
}
```


## Queue
`GET /queue`  
Returns the current print queue

### Response
```json
{
    "success": true|false,
    "jobs": [
        {
            "PrinterName": "Microsoft Print to PDF",
            "DocumentNumber": "{{uuid}}",
            "PrinterStatuses": [], //Array of printer statuses
            "IsPrinting": true|false,
            "IsError": true|false,
            "IsPaused": true|false,
        }
    ]
}
```


## Print
`POST /print`
Prints a document to the printer.

### Request
```json
{
    "success": true|false,
    "jobId": "{{uuid}}",
}
```