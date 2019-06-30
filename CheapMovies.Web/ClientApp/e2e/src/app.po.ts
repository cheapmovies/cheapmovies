import { browser, by, element } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get('/');
  }

  getMainHeading() {
    return element(by.css('app-root h1')).getText();
  }

  getAlphabeticalButton() {
    return element(by.id('alphabetical-button'));
  }

  getOldestButton() {
    return element(by.id('oldest-button'));
  }

  getNewestButton() {
    return element(by.id('newest-button'));
  }

  getOrderByLabel() {
    return element(by.id('order-by-label')).getText();
  }
}
