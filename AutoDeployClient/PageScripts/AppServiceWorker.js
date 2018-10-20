/*
*
*  Push Notifications codelab
*  Copyright 2015 Google Inc. All rights reserved.
*
*  Licensed under the Apache License, Version 2.0 (the "License");
*  you may not use this file except in compliance with the License.
*  You may obtain a copy of the License at
*
*      https://www.apache.org/licenses/LICENSE-2.0
*
*  Unless required by applicable law or agreed to in writing, software
*  distributed under the License is distributed on an "AS IS" BASIS,
*  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
*  See the License for the specific language governing permissions and
*  limitations under the License
*
*/

/* eslint-env browser, serviceworker, es6 */

'use strict';

/* eslint-disable max-len */

const applicationServerPublicKey = "BNryFoXl4sl4CY1Sx5IcNisuc9ROob4h_kPQnw3D1vKjDR8V1GvZnxRLiH98SrOQvHxXZ05ibGR-3BD8EIGDcMM";

/* eslint-enable max-len */

function urlB64ToUint8Array(base64String) {
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding)
      .replace(/\-/g, '+')
      .replace(/_/g, '/');

    const rawData = window.atob(base64);
    const outputArray = new Uint8Array(rawData.length);

    for (let i = 0; i < rawData.length; ++i) {
        outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
}

self.addEventListener('push', function (event) {
    console.log('[Service Worker] Push Received.');
    console.log(`[Service Worker] Push had this data: "${event.data.text()}"`);

    var title = 'Pass push msg';
    const options = {
        body: event.data.text(),
        icon: 'images/icon.png',
        badge: 'images/badge.png'
    };
    //PassDataToClient(event.data.text())

    const payload = event.data.text()
    console.log("[Service Worker] pass payload data", payload)
    const passUrl = 'http://localhost:52131/api/Relay/PassPushMsg'
    console.log("[Service Worker] pass to: " + passUrl)

    fetch(passUrl, {
        method: 'post',
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        },
        body: payload
    })
      .then(function (data) {
          console.log('[Service Worker] Request succeeded with JSON response', JSON.stringify(data));
      })
      .catch(function (error) {
          console.log('[Service Worker] Request failed', error);
      });

    event.waitUntil(self.registration.showNotification(title, options));
});

function PassDataToClient(pushMsg) {

    payload = {
        'pushMsgData': pushMsg
    }

    console.log("pass payload data", JSON.stringify(payload))

    const myRequest = new Request('http://localhost:52131/api/Relay/PassPushMsg', { method: 'POST', body: JSON.stringify(payload) });
    fetch(myRequest)
      .then(response => {
          if (response.status === 200) {
              return response.json();
          } else {
              throw new Error('Something went wrong on api server!');
          }
      })
      .then(response => {
          console.debug(response);
          // ...
      }).catch(error => {
          console.error(error);
      });
}

self.addEventListener('notificationclick', function (event) {
    console.log('[Service Worker] Notification click Received.');

    event.notification.close();

    event.waitUntil(
      clients.openWindow('https://developers.google.com/web/')
    );
});

self.addEventListener('pushsubscriptionchange', function (event) {
    console.log('[Service Worker]: \'pushsubscriptionchange\' event fired.');
    const applicationServerKey = urlB64ToUint8Array(applicationServerPublicKey);
    event.waitUntil(
      self.registration.pushManager.subscribe({
          userVisibleOnly: true,
          applicationServerKey: applicationServerKey
      })
      .then(function (newSubscription) {
          // TODO: Send to application server
          console.log('[Service Worker] New subscription: ', newSubscription);
      })
    );
});