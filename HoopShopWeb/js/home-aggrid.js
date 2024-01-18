// Initialize AG-Grid
var gridOptions1 = {
    columnDefs: [
        {
            headerName: 'OWNER INFORMATION',
            children: [
                { headerName: 'ORDERID', field: 'shid' },
                { headerName: 'JOBCODE', field: 'transcode' },
                { headerName: 'ORDER DATE', field: 'dcreated' },
            ]
        },
        {
            headerName: 'SERVICE AVAILED',
            children: [
                { headerName: 'CREDIT', field: 'credit' },
                { headerName: 'DETAILS', field: 'details' },
                { headerName: 'REMAINING TIME', field: 'countexpiration' } // Timer column
            ]
        },
        {
            headerName: 'SERVICE FINDINGS',
            children: [
                { headerName: 'STATUS', field: 'status' },
                { headerName: 'FINDINGS', field: 'findings' }
            ]
        },
        {
            headerName: 'SHOE BRAND',
            children: [
                { headerName: 'SHOE', field: 'brand' },
            ]
        },
        {
            headerName: 'Proceed',
            cellRenderer: function (params) {
                var button = document.createElement('button');
                button.innerHTML = 'Proceed';
                button.classList.add('proceed-button'); // Adding a class for styling
                button.addEventListener('click', function () {
                    var selectedShoeId = params.data.shid;
                    window.location.href = `shoeverification.aspx?shoe_id=${selectedShoeId}`;
                });
                return button;
            }
        }
    ],
    defaultColDef: {
        resizable: true,
        suppressSizeToFit: true,
        width: 150,
        enableRowGroup: true,
        enablePivot: true,
        enableValue: true,
    },
    rowData: [], // Initial empty data
    // Other AG-Grid configuration options
};

// Function to calculate countdown for "24 HOURS VERIFICATION PROCESS"
function calculateCountdown(data) {
    data.forEach(function (item) {
        if (item.details === '24 HOURS VERIFICATION PROCESS') {
            var createdDate = new Date(item.dcreated); // Assuming date_created is in a valid date format
            var expirationDate = new Date(createdDate.getTime() + (24 * 60 * 60 * 1000)); // Adding 24 hours
            var now = new Date();
            var timeDifference = expirationDate - now;

            // Calculate remaining hours and minutes
            var hours = Math.floor(timeDifference / (1000 * 60 * 60));
            var minutes = Math.floor((timeDifference % (1000 * 60 * 60)) / (1000 * 60));

            item.countexpiration = `${hours}h ${minutes}m`;
        }
    });
}

// Fetch data from the server and populate the grid
function fetchAndPopulateDataOwner() {
    fetch('http://localhost/shoemarketcheckph/api/transaction.php')
        .then(response => response.json())
        .then(data => {
            calculateCountdown(data); // Calculate countdown
            gridOptions1.api.setRowData(data);
            console.log(data)
        })
        .catch(error => {
            console.error('Error fetching data:', error);
        });
}

// Call the function to fetch and populate data when the page loads
document.addEventListener('DOMContentLoaded', function () {
    var gridDivClient = document.querySelector('#gridClient');
    new agGrid.Grid(gridDivClient, gridOptions1);

    // Fetch and populate data
    fetchAndPopulateDataOwner();
});
