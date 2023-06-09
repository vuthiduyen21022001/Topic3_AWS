$(function () {
    var l = abp.localization.getResource('UploadAws');
    var createModal = new abp.ModalManager(abp.appPath + 'Sales/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Sales/EditModal');
    var dataTable = $('#SalesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(uploadAws.sales.sale.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('SaleDeletionConfirmationMessage', data.record.name);
                                    },
                                    action: function (data) {
                                        uploadAws.sales.sale
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }

                            ]
                    }
                },
   //{
                //    text: l('Delete'),
                //    confirmMessage: function (data) {
                //        return l('SaleDeletionConfirmationMessage', data.record.name);
                //    },
                //    action: function (data) {
                //        uploadAws.sales.sale
                //            .delete(data.record.id)
                //            .then(function () {
                //                abp.notify.info(l('SuccessfullyDeleted'));
                //                dataTable.ajax.reload();
                //            });
                //    }
                //}
                    
                 

              
                   
                {
                    title: l('AccessKeyId'),
                    data: "accessKeyId"
                },
                {
                    title: l('SecretAccessKey'),
                    data: "secretAccessKey"
                },
                {
                    title: l('UseCredentials'),
                    data: "useCredentials",
                    render: function (data) {
                        if (data) {
                            return l('Yes');
                        } else {
                            return l('No');
                        }
                    }
                },

                {
                    title: l('UseTemporaryCredentials'),
                    data: "useTemporaryCredentials",
                    render: function (data) {
                        if (data) {
                            return l('Yes');
                        } else {
                            return l('No');
                        }
                    }
                },

                {
                    title: l('UseTemporaryFederatedCredentials'),
                    data: "useTemporaryFederatedCredentials",
                    render: function (data) {
                        if (data) {
                            return l('Yes');
                        } else {
                            return l('No');
                        }
                    }
                },

                {
                    title: l('ProfileName'),
                    data: "profileName"
                },
                {
                    title: l('ProfilesLocation'),
                    data: "profilesLocation"
                },
                {
                    title: l('Region'),
                    data: "region"
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Policy'),
                    data: "policy"
                },

                {
                    title: l('DurationSeconds'),
                    data: "durationSeconds",
                    render: function (data) {
                        return parseFloat(data).toLocaleString();
                    }
                },
                {
                    title: l('ContainerName'),
                    data: "containerName"
                },
                {
                    title: l('CreateContainerIfNotExists'),
                    data: "CreateContainerIfNotExists",
                    render: function (data) {
                        if (data) {
                            return l('No');
                        } else {
                            return l('Yes');
                        }
                    }
                }
              
             ]        })
    );
    var createModal = new abp.ModalManager(abp.appPath + 'Sales/CreateModal');

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewSaleButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});


