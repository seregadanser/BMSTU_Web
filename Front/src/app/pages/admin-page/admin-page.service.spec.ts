import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AdminPageService, Place, PlaceWI, AdminCompose } from './admin-page.service';

describe('AdminPageService', () => {
  let service: AdminPageService;
  let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AdminPageService],
    });

    service = TestBed.inject(AdminPageService);
    httpTestingController = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpTestingController.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should get places', () => {
    const mockPlacesResponse = {
      results: [
        { id: 1, numberStay: 2, numberLayer: 3, size: 100 },
        { id: 2, numberStay: 1, numberLayer: 4, size: 150 },
      ],
      total: 2,
      per_page: 10,
    };

    service.getPlaces().subscribe((response) => {
      expect(response.items.length).toBe(2);
      expect(response.items[0]).toEqual(jasmine.any(Place));
      expect(response.pages).toBe(1);
    });

    const req = httpTestingController.expectOne(`${service['apiPlacesUrl']+"?page=1&per_page=2"}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockPlacesResponse);
  });

  it('should add a new place', () => {
    const mockPlaceWI: PlaceWI = { NumberStay: 2, NumberLayer: 3, Size: 100 };

    service.addPlace(mockPlaceWI).subscribe((response) => {
      expect(response).toBeTruthy(); // Add your specific expectations here
    });

    const req = httpTestingController.expectOne(`${service['apiPlacesUrl']}`);
    expect(req.request.method).toBe('POST');
    req.flush({}); // Mock response for the addPlace request
  });


  it('should get inventory products', () => {
    const mockInventoryProductsResponse = {
      results: [
        { productId: 1, name: 'Product A', dateCome: '2023-01-01', dateProduction: '2023-01-01', inventoryNumber: 123, placeId: 'Place123' },
        { productId: 2, name: 'Product B', dateCome: '2023-01-02', dateProduction: '2023-01-02', inventoryNumber: 456, placeId: 'Place456' },
      ],
      total: 2,
      per_page: 10,
    };

    service.getInventoryProducts().subscribe((response) => {
      expect(response.items.length).toBe(2);
      expect(response.items[0]).toEqual(jasmine.any(AdminCompose));
      expect(response.pages).toBe(1);
    });

    const req = httpTestingController.expectOne(`${service['apiItemsUrl']+"?page=1&per_page=2"}`);
    expect(req.request.method).toBe('GET');
    req.flush(mockInventoryProductsResponse);
  });

  it('should add inventory product', () => {
    const mockProductData: AdminCompose = {
      ProductId: 1,
      Name: 'Product A',
      DateCome: '2023-01-01',
      DateProduction: '2023-01-01',
      InventoryNumber: 123,
      PlaceId: 'Place123'
    };

    service.addInventoryProduct(mockProductData).subscribe((response) => {
      expect(response).toBeTruthy(); // Add your specific expectations here
    });

    const req = httpTestingController.expectOne(`${service['apiItemsUrl']}`);
    expect(req.request.method).toBe('POST');
    req.flush({}); // Mock response for the addInventoryProduct request
  });

  it('should get inventory product by ID', () => {
    const mockProductId = 1;

    service.getInventoryProductById(mockProductId).subscribe((response) => {
      expect(response).toEqual(jasmine.any(AdminCompose));
      // Add your specific expectations here based on the mock response
    });

    const req = httpTestingController.expectOne(`${service['apiItemsUrl']}/${mockProductId}`);
    expect(req.request.method).toBe('GET');
    req.flush(jasmine.any(AdminCompose)); // Mock response for the getInventoryProductById request
  });

  it('should delete inventory product by ID', () => {
    const mockProductId = 1;

    service.deleteInventoryProductById(mockProductId).subscribe((response) => {
      // Add your specific expectations here based on the mock response
    });

    const req = httpTestingController.expectOne(`${service['apiItemsUrl']}/${mockProductId}`);
    expect(req.request.method).toBe('DELETE');
    req.flush({}); // Mock response for the deleteInventoryProductById request
  });
});