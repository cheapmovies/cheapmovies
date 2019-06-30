import { AppPage } from './app.po';
import { browser } from 'protractor';

describe('App', () => {
  let page: AppPage;

  beforeEach(() => {
    page = new AppPage();
  });

  it('should display title', () => {
    page.navigateTo();
    expect(page.getMainHeading()).toEqual('Cheap Movies');
  });

  it('should sort alphabetically', () => {
    browser.ignoreSynchronization = true;
    browser.sleep(2000);
    page.getAlphabeticalButton().click().then(function () {
      expect(page.getOrderByLabel()).toEqual('Ordered by: title asc');
    });
  });

  it('should sort by year oldest', () => {
    browser.ignoreSynchronization = true;
    browser.sleep(2000);
    page.getOldestButton().click().then(function () {
      expect(page.getOrderByLabel()).toEqual('Ordered by: year asc');
    });
  });

  it('should sort by year newest', () => {
    browser.ignoreSynchronization = true;
    browser.sleep(2000);
    page.getNewestButton().click().then(function () {
      expect(page.getOrderByLabel()).toEqual('Ordered by: year desc');
    });
  });

});
