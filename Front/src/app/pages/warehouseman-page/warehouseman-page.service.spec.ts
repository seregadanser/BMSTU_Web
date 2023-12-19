import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { WarehousemanPageService, UsingProductsPlaces } from './warehouseman-page.service';

describe('WarehousemanPageService', () => {
  let service: WarehousemanPageService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [WarehousemanPageService]
    });

    service = TestBed.inject(WarehousemanPageService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getUsingProductsPlaces', () => {
    it('should return usingProductsPlaces and total pages', () => {
      const mockResponse = {
        results: [
          { name: 'John', secondName: 'Doe', login: 'johndoe', inventoryNumber: 1, numberStay: 5, numberLayer: 'A', dateOfStart: '2023-01-01T12:00:00Z' },
          // Add more mock data as needed
        ],
        total: 10,
        per_page: 2
      };

      service.getUsingProductsPlaces().subscribe(response => {
        expect(response.items.length).toBe(1); // Adjust based on the number of mock items
        expect(response.pages).toBe(5); // Math.ceil(10 / 2)
      });

      const req = httpTestingController.expectOne('http://localhost:5001/usingProductsPlaces?page=1&per_page=2');
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
    });

    // Add more test cases as needed
  });

  describe('getUsingProductsPlaceById', () => {
    it('should return a specific usingProductsPlace by ID', () => {
      const mockId = '1';
      const mockResponse = {
        name: 'John',
        secondName: 'Doe',
        login: 'johndoe',
        inventoryNumber: 1,
        numberStay: 5,
        numberLayer: 'A',
        dateOfStart: '2023-01-01T12:00:00Z'
      };

      service.getUsingProductsPlaceById(mockId).subscribe(response => {
        expect(response).toEqual(mockResponse);
      });

      const req = httpTestingController.expectOne(`http://localhost:5001/usingProductsPlaces/${mockId}`);
      expect(req.request.method).toBe('GET');
      req.flush(mockResponse);
    });

    // Add more test cases as needed
  });

  describe('deleteUsingProductsPlaceById', () => {
    it('should delete a specific usingProductsPlace by ID', () => {
      const mockId = '1';

      service.deleteUsingProductsPlaceById(mockId).subscribe(response => {
        // The response from a delete request is often empty or a success message.
        // You can add expectations based on the specific API behavior.
        expect(response).toBeTruthy();
      });

      const req = httpTestingController.expectOne(`http://localhost:5001/usingProductsPlaces/${mockId}`);
      expect(req.request.method).toBe('DELETE');
      req.flush({}); // Assuming an empty response for a successful delete
    });

    // Add more test cases as needed
  });
});
