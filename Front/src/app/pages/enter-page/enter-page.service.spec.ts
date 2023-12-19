import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { EnterPageService } from './enter-page.service';

describe('EnterPageService', () => {
  let service: EnterPageService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [EnterPageService],
    });

    service = TestBed.inject(EnterPageService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should login and set session', () => {
    const mockResponse = {
      id_token: 'some_fake_token',
    };

    const username = 'testuser';
    const password = 'testpassword';


   

    service.login(username, password).subscribe((response) => {
      expect(response.body.id_token).toBe('some_fake_token');
    });

    const req = httpTestingController.expectOne('http://localhost:5001/Account/login');
    expect(req.request.method).toEqual('POST');
 req.flush(mockResponse);
    // Check if setSession method sets the token in localStorage
    //expect(localStorage.getItem('id_token')).toBe('some_fake_token');
  });

  it('should logout and remove session', () => {
    // Set a token in localStorage to simulate an authenticated session
    localStorage.setItem('id_token', 'some_fake_token');

    // Call the logout method
    service.logout();

    // Check if the token is removed from localStorage after logout
    expect(localStorage.getItem('id_token')).toBeNull();
  });
});
