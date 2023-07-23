/**
 * @license MIT
 * @author codewithsadee <mohammadsadee24@gmail.com>
 * @copyright codewithsadee 2023
 */


'use strict';

/**
 * Fetch data from server
 * @param {*} url API Url [required]
 * @param {*} successCallback Success callback [required]
 * @param {*} errorCallback Error callback [optional]
 */

export async function fetchData(url, successCallback, errorCallback) {

  const response = await fetch(url);

  if (response.ok) {
    const data = await response.json();
    successCallback(data);
  } else {
    const error = await response.json();
    errorCallback && errorCallback(error);
  }

}