import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { WorkerPageService, WorkerLookCompose, WorkerLookComposeWS } from './worker-page.service';

describe('WorkerPageService', () => {
  let service: WorkerPageService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [WorkerPageService]
    });

    service = TestBed.inject(WorkerPageService);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  it('should retrieve free inventory products', () => {
    const mockData = {
      results: [
        {
          inventory_number: 1,
          dateCome: '2023-01-01',
          dateProduction: '2023-01-01',
          name: 'Product 1',
          dateStart: '2023-01-02'
        },
      ],
      total: 10,
      per_page: 2
    };

    service.getFreeInventoryProducts(true).subscribe(data => {
      expect(data.items.length).toBe(1);
      expect(data.pages).toBe(5);
    });

    const req = httpMock.expectOne(`${service["apiFreeInventoryProductsUrl"]}?page=1&per_page=2&using_filter=true`);
    expect(req.request.method).toBe('GET');
    req.flush(mockData);
  });

  it('should add a free inventory product', () => {
    const mockProduct: WorkerLookComposeWS = {
      Inventory_number: 1,
      DateCome: '2023-01-01',
      DateProduction: '2023-01-01',
      Name: 'Product 1',
    };

    service.addFreeInventoryProduct(mockProduct).subscribe();

    const req = httpMock.expectOne(`${service["apiFreeInventoryProductsUrl"]}`);
    expect(req.request.method).toBe('POST');
    expect(req.request.body).toEqual(mockProduct);
    req.flush({});
  });


});

