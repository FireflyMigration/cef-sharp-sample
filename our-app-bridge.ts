/* eslint-disable @typescript-eslint/ban-ts-comment */
//@ts-ignore
declare const CefSharp
//@ts-ignore
declare const helper

let returnMockData = false
//@ts-ignore
if (window.cefSharp) CefSharp.BindObjectAsync("helper")
else returnMockData = true

export async function aButtonWasClicked(text: string): Promise<void> {
  if (returnMockData) {
    alert("Menu opened " + text)
    return
  }
  return helper.aButtonWasClicked(text)
}

export async function getCustomerName(customerId: string): Promise<string> {
  if (returnMockData) return "Name of " + customerId
  return helper.getCustomerName(customerId)
}

export interface Statistics {
  years: number[]
  values: number[]
}
export async function getStatistics(): Promise<Statistics> {
  if (returnMockData)
    return {
      years: [1997],
      values: [5],
    }
  return helper.getStatistics()
}
