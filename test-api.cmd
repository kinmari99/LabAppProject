@echo off
echo =========== DEVICE ===========

echo --- GET all devices ---
curl  -X GET https://localhost:7199/api/device -H "X-App-Client: test"
echo.

echo --- POST create new device ---
curl  -X POST https://localhost:7199/api/device -H "Content-Type: application/json" -d "{\"name\":\"Mikroskop\",\"model\":\"MX-200\",\"serialNumber\":\"ABC123\",\"isOperational\":true}" -H "X-App-Client: test"
echo.

echo --- GET device by ID (1) ---
curl  -X GET https://localhost:7199/api/device/1 -H "X-App-Client: test"
echo.

echo --- PUT update device (1) ---
curl  -X PUT https://localhost:7199/api/device/1 -H "Content-Type: application/json" -d "{\"isOperational\":false}" -H "X-App-Client: test"
echo.

echo --- DELETE device (1) ---
curl  -X DELETE https://localhost:7199/api/device/1 -H "X-App-Client: test"
echo.

echo =========== DIAGNOSTICIAN ===========

echo --- GET all diagnosticians ---
curl  -X GET https://localhost:7199/api/diagnostician -H "X-App-Client: test"
echo.

echo --- POST create diagnostician ---
curl  -X POST https://localhost:7199/api/diagnostician -H "Content-Type: application/json" -d "{\"firstName\":\"Anna\",\"lastName\":\"Nowak\",\"email\":\"anna@lab.com\",\"pwzdl\":\"12345\",\"specialization\":\"Hematology\"}" -H "X-App-Client: test"
echo.

echo --- GET diagnostician by ID (1) ---
curl  -X GET https://localhost:7199/api/diagnostician/1 -H "X-App-Client: test"
echo.

echo --- PUT update diagnostician (1) ---
curl  -X PUT https://localhost:7199/api/diagnostician/1 -H "Content-Type: application/json" -d "{\"firstName\":\"Anna\",\"lastName\":\"Nowak\",\"email\":\"anna@lab.com\",\"pwzdl\":\"12345\",\"specialization\":\"Virology\"}" -H "X-App-Client: test"
echo.

echo --- DELETE diagnostician (1) ---
curl  -X DELETE https://localhost:7199/api/diagnostician/1 -H "X-App-Client: test"
echo.

echo =========== EXAMINATION ===========

echo --- GET all examinations ---
curl  -X GET https://localhost:7199/api/examination -H "X-App-Client: test"
echo.

echo --- POST create examination ---
curl  -X POST https://localhost:7199/api/examination -H "Content-Type: application/json" -d "{\"name\":\"Glukoza\",\"description\":\"Badanie poziomu glukozy\",\"unit\":\"mg/dL\",\"lowerRange\":\"70\",\"upperRange\":\"110\",\"price\":50,\"orderId\":1,\"deviceId\":1}" -H "X-App-Client: test"
echo.

echo --- GET examination by ID (1) ---
curl  -X GET https://localhost:7199/api/examination/1 -H "X-App-Client: test"
echo.

echo --- PUT update examination (1) ---
curl  -X PUT https://localhost:7199/api/examination/1 -H "Content-Type: application/json" -d "{\"name\":\"Glukoza\",\"description\":\"Opis zmieniony\",\"unit\":\"mg/dL\",\"lowerRange\":\"70\",\"upperRange\":\"110\",\"price\":60}" -H "X-App-Client: test"
echo.

echo --- DELETE examination (1) ---
curl  -X DELETE https://localhost:7199/api/examination/1 -H "X-App-Client: test"
echo.

echo.
pause
