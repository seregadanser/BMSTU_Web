import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HradminPageService, MyWorker, MyWorkerP } from './hradmin-page.service';

describe('HradminPageService', () => {
  let service: HradminPageService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [HradminPageService],
    });

    service = TestBed.inject(HradminPageService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });
  it('should get columns', () => {
    const columns = service.getColumns();
    expect(columns).toBeTruthy();
    // Add more relevant expectations based on your implementation
  });

  it('should get users', () => {
    const mockResponse = {
      results: [
        { id: 1, login: 'user1', name: 'Alice', secondName: '', position: '', dateOfBirthday: '', numberOfCome: 0 },
      ],
      total: 1,
      per_page: 2,
    };

    service.getUsers().subscribe((data) => {
      expect(data.items.length).toBe(1);
      expect(data.pages).toBe(1);
    });

    const req = httpTestingController.expectOne('http://localhost:5001/persons?page=1&per_page=2');
    expect(req.request.method).toEqual('GET');
    req.flush(mockResponse);
  });

  it('should add user', () => {
    const mockUser: MyWorkerP = { Id: 1, Login: 'newUser', Name: 'Bob', Second_Name: '', Position: '', Date_of_birthday: '', password: 'password', Come: 0 };

    service.addUser(mockUser).subscribe();

    const req = httpTestingController.expectOne('http://localhost:5001/persons');
    expect(req.request.method).toEqual('POST');
    req.flush({});
  });

});
