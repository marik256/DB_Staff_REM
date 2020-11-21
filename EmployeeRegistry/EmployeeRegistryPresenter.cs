using DB_Staff_REM.EventHub;
using DB_Staff_REM.Messages;
using System;
using System.Data;

namespace DB_Staff_REM.EmployeeRegistry
{
    public class EmployeeRegistryPresenter : IEventHandler<NewRowEditCompleted>, IEventHandler<RowEditCompleted>
    {
        private readonly EmployeeStorage _dataStorage;
        private readonly IEventHub _eventHub;
        private EmployeeRegistryView _view;
        private readonly EmployeeRegistryModel _model;

        internal EmployeeRegistryPresenter(
            IEventHub eventHub,
            EmployeeRegistryModel model, 
            EmployeeStorage dataStorage)
        {
            _eventHub = eventHub;
            _model = model;
            _dataStorage = dataStorage;
        }

        public void BindView(EmployeeRegistryView view)
        {
            _view = view;
            _view.AttachGridCollection(_model.Employees);
        }

        public void DeleteCurrentEmployee()
        {
            var id = _view.CurrentEmployeeId;
            _dataStorage.Delete(id);
            FillAllEmployees();
        }

        public void FillEmployeesByPosition(int positionId)
        {
            RefetchEmployees(storage => storage.SelectByPosition(positionId));
        }

        public void FillAllEmployees()
        {
            RefetchEmployees(storage => storage.SelectAll());
        }

        public void ShowUpdateEmployeeDialog() 
        {
            _eventHub.Publish(new ShowRecordEdit(new Employee
            {
                FirstName = _view.CurrentFirstName,
                Surname = _view.CurrentSurname,
                Address = _view.CurrentAddress,
                Patronimic = _view.CurrentPatronimic,
                PositionTitle = _view.CurrentPositionTitle
            }));
        }

        public void ShowInsertEmployeeDialog() 
        {
            _eventHub.Publish(new ShowNewRecordEdit());
        }

        private void RefetchEmployees(Func<EmployeeStorage, IDataReader> fetchDataFunc)
        {
            _model.Employees.Clear();
            using (var dataSet = fetchDataFunc(_dataStorage))
            {
                _model.Employees.Load(dataSet);
            }
        }

        void IEventHandler<NewRowEditCompleted>.Handle(NewRowEditCompleted message)
        {
            var employee = message.RowData;
            _dataStorage.Insert(employee);
            FillAllEmployees();
        }

        void IEventHandler<RowEditCompleted>.Handle(RowEditCompleted message)
        {
            var employee = message.RowData;
            var id = _view.CurrentEmployeeId;
            _dataStorage.Update(id, employee);
            FillAllEmployees();
        }
    }
}
